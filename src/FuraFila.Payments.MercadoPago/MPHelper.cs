using FuraFila.Domain.Payments;
using FuraFila.Payments.MercadoPago.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.MercadoPago
{
    public static class MPHelper
    {
        public static string GetRedirectUrl(this IPaymentService service, string url, string sandboxUrl, MercadoPagoOptions options)
        {
            return options.IsSandbox ? sandboxUrl : url;
        }
    }
}
