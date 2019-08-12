using FuraFila.Domain.Models;
using FuraFila.Payments.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Commands
{
    public class CreatePaymentCommandResponse
    {
        public PaymentRequestRedirect PaymentRequest { get; set; }
    }
}
