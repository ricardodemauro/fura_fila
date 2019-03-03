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
            var body = new CheckoutRequest();

            body.Sender = new Sender
            {
                Name = request.Customer.Name,
                Email = request.Customer.Email
            };

            body.Currency = Constants.CURRENCY_BRL;

            body.Items = new Item[]
            {
                new Item
                {
                    Amount = 1,
                    Description = request.Order.Description,
                    Id = 1234,
                    Quantity = 1,
                    ShippingCost = 0,
                    Weight = 0
                }
            };

            body.RedirectURL = _options.CallbackUrl;

            body.ExtraAmount = 0;
            body.Reference = "" + request.Order.Id;

            body.Shipping = null;

            body.Timeout = 25;
            body.MaxAge = int.MaxValue;
            body.MaxUses = 5;
            body.Receiver = new Receiver
            {
                Email = request.Customer.Email
            };

            body.EnableRecover = false;

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
