using FuraFila.Domain.Payments;
using FuraFila.Payments.Core;
using FuraFila.Payments.MercadoPago;
using FuraFila.Payments.MercadoPago.Configuration;
using FuraFila.Payments.MercadoPago.Services;
using FuraFila.WebApp.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.WebApp
{
    public static class Bootstrapper
    {
        internal static void RegisterHandlers(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<PaymentRequestHandler>();
        }

        internal static void RegisterPaymentServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MercadoPagoOptions>(cfg =>
            {
                cfg.AccessToken = configuration["MercadoLivre:AccessToken"];
                cfg.IsSandbox = bool.Parse(configuration["MercadoLivre:IsSandbox"]);
                cfg.CallbackUrl = configuration["MercadoLivre:CallbackUrl"];
            });

            services.AddHttpClient<MPHttpService>("MP", c =>
            {
                c.BaseAddress = new Uri("https://api.mercadopago.com");
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddSingleton<IPaymentService, MPPaymentService>();

            services.AddSingleton<PaymentServiceLocator>();
        }
    }
}
