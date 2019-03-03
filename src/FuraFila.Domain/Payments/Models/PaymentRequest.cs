using FuraFila.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Payments.Models
{
    public class PaymentRequest
    {
        public Customer Customer { get; set; }

        public Order Order { get; set; }
    }
}
