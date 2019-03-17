using FuraFila.Domain.Payments;
using FuraFila.Domain.Payments.Interfaces;
using FuraFila.Payments.MercadoPago;
using FuraFila.Payments.PagSeguro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.Core
{
    public class PaymentServiceLocator
    {
        private readonly IServiceProvider _svcProvider;

        public PaymentServiceLocator(IServiceProvider serviceProvider)
        {
            _svcProvider = serviceProvider;
        }

        public IPaymentService this[PaymentBroker broker]
        {
            get
            {
                switch (broker)
                {
                    case PaymentBroker.MercadoPago:
                        return _svcProvider.GetService(typeof(MPPaymentService)) as IPaymentService;
                    case PaymentBroker.PagSeguro:
                        return _svcProvider.GetService(typeof(PagSeguroPaymentService)) as IPaymentService;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(broker));
                }
            }
        }
    }
}
