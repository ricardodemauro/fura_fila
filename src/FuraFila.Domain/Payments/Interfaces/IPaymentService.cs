using FuraFila.Domain.Payments.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FuraFila.Domain.Payments.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> CreatePaymentRequest(PaymentRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }
}
