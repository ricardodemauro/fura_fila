using FuraFila.Domain.Commands;
using FuraFila.Domain.Payments;
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

        public async Task<CreatePaymentCommandResponse> CreatePaymentRequest(CreatePaymentCommandRequest request)
        {
            var body = new PreferenceRequest();
            body.Items = new List<Models.Item>();

            Models.Item item1 = new Models.Item
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
            //?collection_id=17726033&collection_status=approved&preference_id=194008091-c7230847-f816-4af0-bb27-51d875049520&external_reference=null&payment_type=credit_card&merchant_order_id=963854120

            var rs = await _service.SendRequest(body, _options.AccessToken);

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
