using FuraFila.Domain.Commands;
using FuraFila.Domain.Payments.Models;
using FuraFila.Payments.Core;
using System;
using System.Threading.Tasks;
using FuraFila.Identity;
using MediatR;
using System.Threading;
using FuraFila.Domain.Repositories;
using FuraFila.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FuraFila.Domain.Payments.Interfaces;

namespace FuraFila.WebApp.Application
{
    public class PaymentRequestHandler : IRequestHandler<CreatePaymentCommandRequest, CreatePaymentCommandResponse>
    {
        private readonly IEnumerable<IPaymentService> _paymentServices;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly ILogger<PaymentRequestHandler> _logger;

        public PaymentRequestHandler(IEnumerable<IPaymentService> paymentServices, IGenericRepository<Order> orderRepository, ILogger<PaymentRequestHandler> logger)
        {
            _paymentServices = paymentServices ?? throw new ArgumentNullException(nameof(paymentServices));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommandRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (!request.Broker.HasValue)
                throw new ArgumentNullException(nameof(request.Broker));

            var customer = new Customer
            {
                Email = request.User.GetEmail(),
                Name = request.User.GetName(),
                SurName = request.User.GetSurname(),
                DateOfBirth = request.User.GetDateOfBirth(),
                Phone = request.User.GetPhone()
            };

            var order = await _orderRepository.GetById(request.PublicOrderId, x => x.Items);

            var svc = _paymentServices.GetService(request.Broker.Value);

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
