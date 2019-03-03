using FuraFila.Domain.Commands;
using FuraFila.Domain.Payments;
using FuraFila.Domain.Payments.Interfaces;
using FuraFila.Domain.Payments.Models;
using FuraFila.Payments.MercadoPago.Configuration;
using FuraFila.Payments.MercadoPago.Models;
using FuraFila.Payments.MercadoPago.Preferences;
using FuraFila.Payments.MercadoPago.Services;
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

        public MPPaymentService(MPHttpService service, IOptions<MercadoPagoOptions> options)
        {
            _options = options.Value;
            _service = service;
        }

        public async Task<PaymentResponse> CreatePaymentRequest(PaymentRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var body = new PreferenceRequest();
            body.Items = new List<Item>();

            Item item1 = new Item
            {
                Id = "2334234",
                Description = request.Order.Description,
                Title = request.Order.Description,
                UnitPrice = request.Order.Value,
                Quantity = 1,
                CurrencyId = Constants.CURRENCY
            };

            body.Items.Add(item1);

            body.Payer = new Models.Payer
            {
                Email = request.Customer.Email,
                Name = request.Customer.Name,
                Surname = request.Customer.SurName
            };

            body.BackUrls = new Models.BackUrls
            {
                Failure = _options.CallbackUrl,
                Pending = _options.CallbackUrl,
                Success = _options.CallbackUrl
            };

            body.AutoReturn = AutoReturn.ALL;

            var rs = await _service.SendRequest(body, _options.AccessToken);

            return new PaymentResponse
            {
                RequestRedirect = new Domain.Models.PaymentRequestRedirect
                {
                    Amount = request.Order.Value,
                    Id = rs.Id,
                    RedirectUri = this.GetMPRedirectUrl(rs.InitPoint, rs.SandboxInitPoint, _options)
                }
            };
        }
    }
}
