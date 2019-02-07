using FuraFila.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace FuraFila.Domain.Payments
{
    public interface IPaymentService
    {
        Task<CreatePaymentCommandResponse> CreatePaymentRequest(CreatePaymentCommandRequest request);
    }
}
