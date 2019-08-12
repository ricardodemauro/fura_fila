using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    public class PaymentMethod
    {
        public string Group { get; set; }

        public PaymentMethodType Type { get; set; }

        public PaymentMethodCode Code { get; set; }
    }
}
