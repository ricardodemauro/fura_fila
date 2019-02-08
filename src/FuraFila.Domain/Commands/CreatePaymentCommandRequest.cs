using FuraFila.Domain.Models;
using FuraFila.Payments.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Commands
{
    public class CreatePaymentCommandRequest
    {
        public Customer Customer { get; set; }

        public Order Order { get; set; }

        public PaymentMethods[] PaymentMethods { get; set; }
    }
}
