using FuraFila.Domain.Commands;
using FuraFila.Domain.Payments;
using FuraFila.Payments.PagSeguro.Configuration;
using FuraFila.Payments.PagSeguro.Models;
using FuraFila.Payments.PagSeguro.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<CreatePaymentCommandResponse> CreatePaymentRequest(CreatePaymentCommandRequest request)
        {
            var body = new Checkout();

            body.Sender = new checkoutSender
            {
                name = request.Customer.Name,
                email = request.Customer.Email
            };

            body.Currency = Constants.CURRENCY_BRL;

            body.Items = new CheckoutItem[]
            {
                new CheckoutItem
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

            body.extraAmount = 0;
            body.reference = "" + request.Order.Id;

            body.shipping = null;

            body.timeout = 25;
            body.maxAge = int.MaxValue;
            body.maxUses = 5;
            body.receiver = new checkoutReceiver
            {
                email = request.Customer.Email
            };

            body.enableRecover = false;

            var rs = await _service.SendRequest<Checkout, CheckoutResult>(body, _options.AccessToken);

            return new CreatePaymentCommandResponse
            {
                PaymentRequest = new Domain.Models.PaymentRequest
                {
                    Amount = request.Order.Value,
                    Id = rs.Id,
                    RedirectUri = this.GetRedirectUrl(rs.InitPoint, rs.SandboxInitPoint, _options)
                }
            };
        }
    }
}
