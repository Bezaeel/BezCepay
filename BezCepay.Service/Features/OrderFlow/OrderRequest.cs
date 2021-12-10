using AutoMapper;
using BezCepay.Data.IRepositories;
using BezCepay.Data.Models;
using BezCepay.Service.Communication;
using BezCepay.Service.Features.OrderFlow.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BezCepay.Service.Features.OrderFlow
{
    public class OrderRequest : IOrderRequest
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderRequest> _logger;
        private ServiceResponse apiResponse;

        public OrderRequest(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderRequest> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
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
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "GetAllOrders exception");
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
                    apiResponse.Code = ErrorCodes.Notfound;
                    apiResponse.Message = "Order does not exist";
                    return apiResponse;
                }
                apiResponse.IsSuccess = true;
                apiResponse.Message = "Success";
                apiResponse.Code = ErrorCodes.Success;
                apiResponse.Data = result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "GetOrderById exception");
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
                _logger.LogCritical(ex, "CreateOrder exception");
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
