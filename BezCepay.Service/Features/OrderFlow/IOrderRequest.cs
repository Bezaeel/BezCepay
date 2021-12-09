using BezCepay.Service.Communication;
using BezCepay.Service.Features.OrderFlow.Dtos;
using System.Threading.Tasks;

namespace BezCepay.Service.Features.OrderFlow
{
    public interface IOrderRequest
    {
        Task<ServiceResponse> GetAllOrders();
        Task<ServiceResponse> GetOrderById(int id);
        Task<ServiceResponse> CreateOrder(AddOrderDTO dto);
    }
}
