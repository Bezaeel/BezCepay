using AutoMapper;
using BezCepay.Data.IRepositories;
using BezCepay.Data.Models;
using BezCepay.Service.Communication;
using BezCepay.Service.Features.PaymentFlow.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BezCepay.Service.Features.PaymentFlow
{
    public class PaymentRequest : IPaymentRequest
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentRequest> _logger;
        private ServiceResponse apiResponse;

        public PaymentRequest(IPaymentRepository paymentRepository, IMapper mapper, ILogger<PaymentRequest> logger)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _logger = logger;
            apiResponse = new ServiceResponse();

        }
        public async Task<ServiceResponse> GetAllPayments()
        {
            try
            {
                var result = await _paymentRepository.GetAllAsync();
                apiResponse.IsSuccess = true;
                apiResponse.Message = "Success";
                apiResponse.Code = ErrorCodes.Success;
                apiResponse.Data = result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "GetAllPayments exception");
                apiResponse.IsSuccess = false;
                apiResponse.Code = ErrorCodes.Exception;
                apiResponse.Message = "Error occurred";
            }
            return apiResponse;
        }

        public async Task<ServiceResponse> GetPaymentById(int id)
        {
            try
            {
                var result = await _paymentRepository.GetAsync(x => x.Id == id);
                if(result == null)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Code = ErrorCodes.Notfound;
                    apiResponse.Message = "Payment does not exist";
                    return apiResponse;
                }
                apiResponse.IsSuccess = true;
                apiResponse.Message = "Success";
                apiResponse.Code = ErrorCodes.Success;
                apiResponse.Data = result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "GetPaymentById exception");
                apiResponse.IsSuccess = false;
                apiResponse.Code = ErrorCodes.Exception;
                apiResponse.Message = "Error occurred";
            }
            return apiResponse;
        }

        public async Task<ServiceResponse> CreatePayment(AddPaymentDTO dto)
        {
            try
            {
                var model = _mapper.Map<AddPaymentDTO, Payment>(dto);
                model.CurrencyCode = model.CurrencyCode.ToUpper();
                model.Status = Data.Enums.PaymentStatus.Created;
                model.CreationDate = DateTime.UtcNow;
                _paymentRepository.Add(model);
                await _paymentRepository.SaveShangesAsync();
                apiResponse.Message = "Success";
                apiResponse.IsSuccess = true;
                apiResponse.Code = ErrorCodes.Success;
                apiResponse.Data = model;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "CreatePayment exception");
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
