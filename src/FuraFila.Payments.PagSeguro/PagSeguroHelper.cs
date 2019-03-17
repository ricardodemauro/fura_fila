using FuraFila.Payments.PagSeguro.Commands;
using FuraFila.Payments.PagSeguro.Configuration;
using FuraFila.Payments.PagSeguro.Models;
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

        public static string GetCustomerEmail(this PagSeguroPaymentService service, string originalEmail, PagSeguroOptions options)
        {
            string email = options.IsSandbox ? "something@sandbox.pagseguro.com.br" : originalEmail;

            return email;
        }

        public static Receiver CreateReceiver(this PagSeguroPaymentService service, PagSeguroOptions options)
        {
            return new Receiver
            {
                Email = options.Email
            };
        }

        public static Phone CreateCustomerPhone(this PagSeguroPaymentService service, Domain.Models.Customer customer)
        {
            if (string.IsNullOrEmpty(customer.AreaCode) || string.IsNullOrEmpty(customer.Phone))
                return null;
            return new Phone
            {
                AreaCode = customer.AreaCode,
                Number = customer.Phone
            };
        }

        public static CheckoutRequest CreateNewCheckoutRequest(this PagSeguroPaymentService service, PagSeguroOptions options)
        {
            return new CheckoutRequest
            {
                Currency = Constants.CURRENCY_BRL,
                RedirectURL = options.CallbackUrl,
                ExtraAmount = 0M,
                Timeout = 3600,
                MaxAge = 3600,
                MaxUses = 5,
                Receiver = CreateReceiver(service, options),
                EnableRecover = false
            };
        }
    }
}
