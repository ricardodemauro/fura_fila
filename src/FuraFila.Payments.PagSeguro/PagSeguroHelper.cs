using FuraFila.Payments.PagSeguro.Configuration;
using System;

namespace FuraFila.Payments.PagSeguro
{
    public static class PagSeguroHelper
    {
        public static Uri GetPagSeguroPaymentUrl(this PagSeguroPaymentService service, string transactionCode, PagSeguroOptions options)
        {
            string url = options.IsSandbox ? options.PaymentUrlSandbox : options.PaymentUrl;

            return new Uri(string.Format(url, transactionCode));
        }
    }
}
