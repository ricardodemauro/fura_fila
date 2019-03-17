using FuraFila.Domain.Infrastructure;
using FuraFila.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Commands
{
    public class UpdateTransactionCommandRequest : ServiceRequestBase, IRequestContext
    {
        public string Type { get; set; }

        public PaymentBroker Broker { get; set; }

        public string Code { get; set; }
    }
}
