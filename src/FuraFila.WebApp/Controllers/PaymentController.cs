using FuraFila.Domain.Commands;
using FuraFila.WebApp.Application;
using FuraFila.WebApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FuraFila.WebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentRequestHandler _paymentRequestHandler;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(PaymentRequestHandler paymentRequestHandler, ILogger<PaymentController> logger)
            : base()
        {
            _paymentRequestHandler = paymentRequestHandler ?? throw new ArgumentNullException(nameof(paymentRequestHandler));
            _logger = logger;
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
            rq.Broker = Domain.Payments.PaymentBroker.MercadoPago;

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
            rq.Broker = Domain.Payments.PaymentBroker.PagSeguro;

            CreatePaymentCommandResponse rs = await _paymentRequestHandler.Handle(rq);
            return RedirectPermanent(rs.PaymentRequest.RedirectUri.AbsoluteUri);
        }

        [HttpGet]
        public IActionResult PagSeguroCallback(string transactionId)
        {
            Console.WriteLine($"transaction id = {transactionId}");
            return Ok();
        }


    }
}
