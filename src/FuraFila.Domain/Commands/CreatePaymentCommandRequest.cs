using FuraFila.Domain.Infrastructure;
using FuraFila.Domain.Models;
using FuraFila.Domain.Payments;
using FuraFila.Payments.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Commands
{
    public class CreatePaymentCommandRequest : ServiceRequestBase, IRequest<CreatePaymentCommandResponse>
    {
        public string PublicOrderId { get; set; }

        public PaymentBroker? Broker { get; set; }
    }
}
