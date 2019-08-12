using FuraFila.Domain.Commands;
using FuraFila.Domain.Payments;
using FuraFila.Domain.Payments.Interfaces;
using FuraFila.Domain.Payments.Models;
using FuraFila.Payments.Core;
using FuraFila.Payments.PagSeguro.Configuration;
using FuraFila.Payments.PagSeguro.Models;
using FuraFila.Payments.PagSeguro.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro
{
    public class PagSeguroPaymentService : IPaymentService
    {
        private readonly PagSeguroOptions _options;
        private readonly PagSeguroService _service;
        private readonly ILogger<PagSeguroPaymentService> _logger;

        public PagSeguroPaymentService(ILogger<PagSeguroPaymentService> logger, PagSeguroService service, IOptions<PagSeguroOptions> options)
        {
            _options = options.Value;
            _service = service;
            _logger = logger;
        }

        public PaymentBroker Name => PaymentBroker.PagSeguro;

        public async Task<PaymentResponse> CreatePaymentRequest(PaymentRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var bodyCheckout = this.CreateNewCheckoutRequest(_options);
            bodyCheckout.Sender = new Sender
            {
                Name = request.Customer.Name,
                Email = request.Customer.Email,
                BornDate = request.Customer.DateOfBirth,
                Phone = this.CreateCustomerPhone(request.Customer)
            };

            var orders = request.Order.Items;
            bodyCheckout.Items = new Item[orders.Count];

            for (int i = 0; i < orders.Count; i++)
            {
                var singleOrder = orders[i];
                bodyCheckout.Items[i] = new Item
                {
                    Id = singleOrder.Id,
                    Amount = singleOrder.UnitPrice,
                    Description = singleOrder.Description,
                    Quantity = singleOrder.Quantity,
                    ShippingCost = 0,
                    Weight = 0
                };
            }

            bodyCheckout.Shipping = new Shipping
            {
                AddressRequired = false,
                Type = ShippingType.NaoEspecificado
            };

            bodyCheckout.Reference = request.Order.Id;

            var rs = await _service.Checkout(bodyCheckout, _options.AccessToken, _options.Email, cancellationToken);

            var paymentResponse = new PaymentResponse
            {
                RequestRedirect = new PaymentRequestRedirect
                {
                    Amount = request.Order.UnitPrice,
                    Id = rs.Code,
                    RedirectUri = this.GetPagSeguroPaymentUrl(rs.Code, _options)
                }
            };

            return paymentResponse;
        }
    }
}
