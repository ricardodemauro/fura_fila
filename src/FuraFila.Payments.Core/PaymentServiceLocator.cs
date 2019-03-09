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

        public IPaymentService this[PaymentBrokers broker]
        {
            get
            {
                switch (broker)
                {
                    case PaymentBrokers.MercadoPago:
                        return _svcProvider.GetService(typeof(MPPaymentService)) as IPaymentService;
                    case PaymentBrokers.PagSeguro:
                        return _svcProvider.GetService(typeof(PagSeguroPaymentService)) as IPaymentService;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(broker));
                }
            }
        }
    }
}
