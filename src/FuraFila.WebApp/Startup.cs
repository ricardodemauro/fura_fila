using FuraFila.Repository.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FuraFila.Repository.EF.DependencyInjection;
using FuraFila.Repository.EF.Seeds;
using Microsoft.AspNetCore.Identity;
using FuraFila.Domain.Models;
using Microsoft.AspNetCore.Identity.UI;
using System;
using Microsoft.AspNetCore.Mvc.Authorization;
using FuraFila.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using FuraFila.Domain.Repositories;
using FuraFila.Repository.EF.Repositories;

namespace FuraFila.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddMvc(opts =>
            {
                //opts.Filters.Add(new RequireHttpsAttribute());
                opts.Filters.Add(new AuthorizeFilter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            string connection = Configuration.GetConnectionString("Core");
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(connection));

            services.AddScoped(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));

            services.AddIdentity<ApplicationUser, IdentityRole>(opts => opts.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequiredLength = 6;

                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.Lockout.AllowedForNewUsers = true;

                // User settings.
                opts.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opts.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(opts =>
            {
                opts.Cookie.HttpOnly = true;
                opts.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                opts.LoginPath = "/identity/account/login";
                opts.AccessDeniedPath = "/acesso-negado";
                opts.SlidingExpiration = true;
            });

            services.AddAuthentication()
                .AddFacebook(cfg =>
                {
                    cfg.AppId = Configuration["Authentication:FacebookAppId"];
                    cfg.AppSecret = Configuration["Authentication:FacebookAppSecret"];
                })
                .AddGoogle(o =>
                {
                    o.ClientId = Configuration["Authentication:GoogleAppId"];
                    o.ClientSecret = Configuration["Authentication:GoogleAppSecret"];
                    o.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                    o.ClaimActions.Clear();
                    o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    o.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                    o.ClaimActions.MapJsonKey("urn:google:profile", "link");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                });

            services.AddOptions();
            services.AddHttpClient();

            Bootstrapper.RegisterHandlers(services, Configuration);
            Bootstrapper.RegisterPaymentServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();

                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            ////app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
