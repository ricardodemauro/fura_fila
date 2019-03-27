using FuraFila.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.Core
{
    public class PaymentRequestRedirect
    {
        public decimal Amount { get; set; }

        public string Id { get; set; }

        public Uri RedirectUri { get; set; }

        public PaymentBroker Broker { get; set; }
    }
}
