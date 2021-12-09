using AutoMapper;
using BezCepay.Data.IRepositories;
using BezCepay.Data.Models;
using BezCepay.Service.Communication;
using BezCepay.Service.Features.OrderFlow.Dtos;
using System;
using System.Threading.Tasks;

namespace BezCepay.Service.Features.OrderFlow
{
    public class OrderRequest : IOrderRequest
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private ServiceResponse apiResponse;

        public OrderRequest(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            apiResponse = new ServiceResponse();

        }
        public async Task<ServiceResponse> GetAllOrders()
        {
            try
            {
                var result = await _orderRepository.GetAllAsync();
                apiResponse.IsSuccess = true;
                apiResponse.Message = "Success";
                apiResponse.Code = ErrorCodes.Success;
                apiResponse.Data = result;
            }
            catch (Exception)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Code = ErrorCodes.Exception;
                apiResponse.Message = "Error occurred";
            }
            return apiResponse;
        }

        public async Task<ServiceResponse> GetOrderById(int id)
        {
            try
            {
                var result = await _orderRepository.GetAsync(x => x.Id == id);
                if(result == null)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Code = ErrorCodes.Error;
                    apiResponse.Message = "Asset does not exist";
                    return apiResponse;
                }
                apiResponse.IsSuccess = true;
                apiResponse.Message = "Success";
                apiResponse.Code = ErrorCodes.Success;
                apiResponse.Data = result;
            }
            catch (Exception)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Code = ErrorCodes.Exception;
                apiResponse.Message = "Error occurred";
            }
            return apiResponse;
        }

        public async Task<ServiceResponse> CreateOrder(AddOrderDTO dto)
        {
            try
            {
                var model = _mapper.Map<AddOrderDTO, Order>(dto);
                model.CreatedAt = DateTime.UtcNow;
                _orderRepository.Add(model);
                await _orderRepository.SaveShangesAsync();
                apiResponse.Message = "Success";
                apiResponse.IsSuccess = true;
                apiResponse.Code = ErrorCodes.Success;
                apiResponse.Data = model;
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Code = ErrorCodes.Exception;
                apiResponse.Message = "Error occurred";
            }
            return apiResponse;
        }

        public async Task<ServiceResponse> UpdateAsset(Payment model)
        {
            throw new NotImplementedException();
        }

    }
}
