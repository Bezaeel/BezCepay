using BezCepay.Service.Communication;
using BezCepay.Service.Features.PaymentFlow.Dtos;
using System.Threading.Tasks;

namespace BezCepay.Service.Features.PaymentFlow
{
    public interface IPaymentRequest
    {
        Task<ServiceResponse> GetAllPayments();
        Task<ServiceResponse> GetPaymentById(int id);
        Task<ServiceResponse> CreatePayment(AddPaymentDTO dto);
    }
}
