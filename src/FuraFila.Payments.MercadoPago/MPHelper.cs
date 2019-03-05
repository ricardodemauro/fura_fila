using FuraFila.Domain.Payments;
using FuraFila.Domain.Payments.Interfaces;
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
        public static Uri GetMPRedirectUrl(this MPPaymentService service, string url, string sandboxUrl, MercadoPagoOptions options)
        {
            string uri = options.IsSandbox ? sandboxUrl : url;

            return new Uri(uri);
        }
    }
}
