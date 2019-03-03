using FuraFila.Domain.Payments;
using FuraFila.Payments.Core;
using FuraFila.Payments.MercadoPago;
using FuraFila.Payments.MercadoPago.Configuration;
using FuraFila.Payments.MercadoPago.Services;
using FuraFila.Payments.PagSeguro;
using FuraFila.Payments.PagSeguro.Configuration;
using FuraFila.Payments.PagSeguro.Services;
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
                cfg.AccessToken = configuration["MercadoPago:AccessToken"];
                cfg.IsSandbox = bool.Parse(configuration["MercadoPago:IsSandbox"]);
                cfg.CallbackUrl = configuration["MercadoPago:CallbackUrl"];
            });

            services.AddHttpClient<MPHttpService>("MP", c =>
            {
                c.BaseAddress = new Uri("https://api.mercadopago.com");
            });

            services.AddSingleton<MPPaymentService>();

            services.Configure<PagSeguroOptions>(cfg =>
            {
                cfg.AccessToken = configuration["PagSeguro:Token"];
                cfg.Email = configuration["PagSeguro:Email"];
                cfg.IsSandbox = bool.Parse(configuration["PagSeguro:IsSandbox"]);
                cfg.CallbackUrl = configuration["PagSeguro:CallbackUrl"];

                cfg.PaymentUrl = configuration["PagSeguro:PaymentUrl"];
                cfg.PaymentUrlSandbox = configuration["PagSeguro:PaymentUrlSandbox"];
            });

            services.AddHttpClient<PagSeguroService>("PS", c =>
            {
                var isSandbox = string.Compare(configuration["PagSeguro:IsSandbox"], bool.TrueString, true) == 0;

                if (isSandbox)
                    c.BaseAddress = new Uri(configuration["PagSeguro:ServiceUrlSandbox"]);
                else
                    c.BaseAddress = new Uri(configuration["PagSeguro:ServiceUrl"]);
            });

            services.AddSingleton<PagSeguroPaymentService>();

            services.AddSingleton<PaymentServiceLocator>();
        }
    }
}
