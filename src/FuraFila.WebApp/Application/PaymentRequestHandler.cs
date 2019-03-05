using FuraFila.Domain.Commands;
using FuraFila.Domain.Payments.Models;
using FuraFila.Payments.Core;
using System;
using System.Threading.Tasks;
using FuraFila.Identity;

namespace FuraFila.WebApp.Application
{
    public class PaymentRequestHandler
    {
        private readonly PaymentServiceLocator _locator;

        public PaymentRequestHandler(PaymentServiceLocator locator)
        {
            _locator = locator ?? throw new ArgumentNullException(nameof(locator));
        }

        public async Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommandRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (!request.Broker.HasValue)
                throw new ArgumentNullException(nameof(request.Broker));

            var customer = new Domain.Models.Customer
            {
                Email = request.User.GetEmail(),
                Name = request.User.GetName(),
                SurName = request.User.GetSurname(),
                DateOfBirth = request.User.GetDateOfBirth(),
                Phone = request.User.GetPhone()
            };
            var order = new Domain.Models.Order
            {
                Description = "rango",
                Paid = false,
                Value = 10.4m
            };

            var svc = _locator[request.Broker.Value];

            var paymentRequest = new PaymentRequest
            {
                Customer = customer,
                Order = order
            };

            var paymentResponse = await svc.CreatePaymentRequest(paymentRequest);

            var cmdResponse = new CreatePaymentCommandResponse
            {
                PaymentRequest = paymentResponse.RequestRedirect
            };

            return cmdResponse;
        }
    }
}
