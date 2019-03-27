using FuraFila.Domain.Commands;
using FuraFila.Domain.Payments;
using FuraFila.Domain.Payments.Interfaces;
using FuraFila.Domain.Payments.Models;
using FuraFila.Payments.Core;
using FuraFila.Payments.MercadoPago.Configuration;
using FuraFila.Payments.MercadoPago.Models;
using FuraFila.Payments.MercadoPago.Preferences;
using FuraFila.Payments.MercadoPago.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FuraFila.Payments.MercadoPago
{
    public class MPPaymentService : IPaymentService
    {
        private readonly MercadoPagoOptions _options;
        private readonly MPHttpService _service;
        private readonly ILogger<MPPaymentService> _logger;

        public MPPaymentService(ILogger<MPPaymentService> logger, MPHttpService service, IOptions<MercadoPagoOptions> options)
        {
            _options = options.Value;
            _service = service;
            _logger = logger;
        }

        public PaymentBroker Name => PaymentBroker.MercadoPago;

        public async Task<PaymentResponse> CreatePaymentRequest(PaymentRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var body = new PreferenceRequest();

            var orders = request.Order.Items;
            body.Items = new List<Item>();

            for (int i = 0; i < orders.Count; i++)
            {
                var singleOrder = orders[i];
                body.Items[i] = new Item
                {
                    Id = singleOrder.Id,
                    Description = singleOrder.Description,
                    Title = singleOrder.Description,
                    UnitPrice = singleOrder.UnitPrice,
                    Quantity = singleOrder.Quantity,
                    CurrencyId = Constants.CURRENCY,
                };
            }

            body.Payer = new Payer
            {
                Email = request.Customer.Email,
                Name = request.Customer.Name,
                Surname = request.Customer.SurName
            };

            body.BackUrls = new BackUrls
            {
                Failure = _options.CallbackUrl,
                Pending = _options.CallbackUrl,
                Success = _options.CallbackUrl
            };

            body.AutoReturn = AutoReturn.ALL;

            var rs = await _service.SendRequest(body, _options.AccessToken);

            return new PaymentResponse
            {
                RequestRedirect = new PaymentRequestRedirect
                {
                    Amount = request.Order.UnitPrice,
                    Id = rs.Id,
                    RedirectUri = this.GetMPRedirectUrl(rs.InitPoint, rs.SandboxInitPoint, _options)
                }
            };
        }
    }
}
