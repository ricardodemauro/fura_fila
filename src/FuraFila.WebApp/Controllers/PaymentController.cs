using FuraFila.Domain.Commands;
using FuraFila.WebApp.Application;
using FuraFila.WebApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FuraFila.WebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentRequestHandler _paymentRequestHandler;

        public PaymentController(PaymentRequestHandler paymentRequestHandler)
            : base()
        {
            _paymentRequestHandler = paymentRequestHandler ?? throw new ArgumentNullException(nameof(paymentRequestHandler));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MercadoPago(string publicOrderId)
        {
            var rq = this.CreateServiceRequest<CreatePaymentCommandRequest>();
            rq.PublicOrderId = publicOrderId;
            rq.Broker = Domain.Payments.PaymentBrokers.MercadoPago;

            CreatePaymentCommandResponse rs = await _paymentRequestHandler.Handle(rq);
            return RedirectPermanent(rs.PaymentRequest.RedirectUri.AbsoluteUri);
        }

        [HttpGet]
        public IActionResult MercadoPagoCallback()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PagSeguro(string publicOrderId)
        {
            var rq = this.CreateServiceRequest<CreatePaymentCommandRequest>();
            rq.PublicOrderId = publicOrderId;
            rq.Broker = Domain.Payments.PaymentBrokers.PagSeguro;

            CreatePaymentCommandResponse rs = await _paymentRequestHandler.Handle(rq);
            return RedirectPermanent(rs.PaymentRequest.RedirectUri.AbsoluteUri);
        }

        [HttpGet]
        public IActionResult PagSeguroCallback()
        {
            return View();
        }
    }
}
