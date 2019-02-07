using FuraFila.Domain.Commands;
using FuraFila.WebApp.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IActionResult> MercadoPago()
        {
            CreatePaymentCommandResponse rs = await _paymentRequestHandler.Handle(new CreatePaymentCommandRequest
            {
                Customer = new Domain.Models.Customer
                {
                    Email = "ricardo@email.com"
                },
                Order = new Domain.Models.Order
                {
                    Description = "rango",
                    Paid = false,
                    Value = 10.4m
                },
            });
            // return View(rs.PaymentRequest);
            return RedirectPermanent(rs.PaymentRequest.RedirectUri);
        }

        public IActionResult MercadoPagoCallback()
        {
            return View();
        }
    }
}
