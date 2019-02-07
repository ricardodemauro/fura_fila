using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Models
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }

        public string Id { get; set; }

        public string RedirectUri { get; set; }
    }
}
