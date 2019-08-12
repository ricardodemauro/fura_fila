using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Payments.Interfaces
{
    public static class PaymentServiceExtensions
    {
        public static IPaymentService GetService(this IEnumerable<IPaymentService> services, PaymentBroker name)
        {
            foreach (IPaymentService svc in services)
            {
                if (svc.Name == name)
                    return svc;
            }
            throw new ArgumentOutOfRangeException(nameof(name));
        }
    }
}
