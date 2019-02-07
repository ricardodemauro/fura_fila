using FuraFila.Domain.Commands;
using FuraFila.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.WebApp.Application
{
    public class PaymentRequestHandler
    {
        private readonly IPaymentService _paymentService;

        public PaymentRequestHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        public Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommandRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var rs = _paymentService.CreatePaymentRequest(request);

            return rs;
        }
    }
}
