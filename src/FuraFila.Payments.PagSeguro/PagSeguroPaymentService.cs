using FuraFila.Domain.Commands;
using FuraFila.Domain.Payments.Interfaces;
using FuraFila.Domain.Payments.Models;
using FuraFila.Payments.PagSeguro.Configuration;
using FuraFila.Payments.PagSeguro.Models;
using FuraFila.Payments.PagSeguro.Services;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro
{
    public class PagSeguroPaymentService : IPaymentService
    {
        private readonly PagSeguroOptions _options;
        private readonly PagSeguroService _service;

        public PagSeguroPaymentService(PagSeguroService service, IOptions<PagSeguroOptions> options)
        {
            _options = options.Value;
            _service = service;
        }

        public async Task<PaymentResponse> CreatePaymentRequest(PaymentRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var body = new CheckoutRequest
            {
                Sender = new Sender
                {
                    Name = request.Customer.Name,
                    Email = request.Customer.Email
                },

                Currency = Constants.CURRENCY_BRL,

                Items = new Item[]
                {
                    new Item
                    {
                        Id = "1234",
                        Description = request.Order.Description,
                        Amount = request.Order.Value,
                        Quantity = 1,
                        ShippingCost = 0M,
                        Weight = 0
                    }
                },

                RedirectURL = _options.CallbackUrl,

                ExtraAmount = 0M,
                Reference = "" + request.Order.Id,

                Shipping = null,

                Timeout = 25,
                MaxAge = int.MaxValue,
                MaxUses = 5,
                Receiver = new Receiver
                {
                    Email = request.Customer.Email
                },

                EnableRecover = false
            };

            var rs = await _service.Checkout(body, _options.AccessToken, _options.Email, cancellationToken);

            var paymentResponse = new PaymentResponse
            {
                RequestRedirect = new Domain.Models.PaymentRequestRedirect
                {
                    Amount = request.Order.Value,
                    Id = rs.Code,
                    RedirectUri = this.GetPagSeguroPaymentUrl(rs.Code, _options)
                }
            };

            return paymentResponse;
        }
    }
}
