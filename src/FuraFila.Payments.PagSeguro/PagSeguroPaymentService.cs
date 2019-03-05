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
            var bodyCheckout = this.CreateNewCheckoutRequest(_options);
            bodyCheckout.Sender = new Sender
            {
                Name = request.Customer.Name,
                Email = request.Customer.Email,
                BornDate = request.Customer.DateOfBirth,
                Phone = this.CreateCustomerPhone(request.Customer)
            };
            bodyCheckout.Items = new Item[]
            {
                new Item
                {
                    Id = "1234",
                    Description = request.Order.Description,
                    Amount = request.Order.Value,
                    Quantity = 1,
                    ShippingCost = null,
                    Weight = 0
                }
            };
            bodyCheckout.Shipping = new Shipping
            {
                AddressRequired = false,
                Type = ShippingType.NaoEspecificado
            };

            bodyCheckout.Reference = "" + request.Order.Id;

            var rs = await _service.Checkout(bodyCheckout, _options.AccessToken, _options.Email, cancellationToken);

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
