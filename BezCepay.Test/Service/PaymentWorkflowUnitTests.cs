using System.Collections.Generic;
using AutoMapper;
using BezCepay.Data.Models;
using Moq;
using Xunit;
using BezCepay.Data.Repositories;
using System.Threading.Tasks;
using BezCepay.Service.Mappings;
using BezCepay.Service.Features.PaymentFlow;
using BezCepay.Service.Features.PaymentFlow.Dtos;
using Microsoft.Extensions.Logging;

namespace BezCepay.Test.Service
{
    public class PaymentWorkflowUnitTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<PaymentRequest>> _logger;
        
        static Setup setup;

        public PaymentWorkflowUnitTests()
        {
            setup = new Setup();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Configs());
            });
            mapper = mockMapper.CreateMapper();
            _logger = new Mock<ILogger<PaymentRequest>>();
        }


        [Fact]
        public async Task ShouldCreatePayment()
        {
            using var transaction = setup.dbContext.Database.BeginTransaction();
            
            // Given
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper, _logger.Object);
            AddPaymentDTO dto = new AddPaymentDTO{
                OrderId = 2,
                Amount = 1000,
                CurrencyCode = "NGN",
            };
            var result = await paymentRequest.CreatePayment(dto);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ShouldGetAllPayments()
        {
            // Given
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper, _logger.Object);
            
            var result = await paymentRequest.GetAllPayments();
            var expectedResult = result;
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ShouldGetPaymentById()
        {
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper, _logger.Object);
            
            var result = await paymentRequest.GetPaymentById(1);
            var expectedResult = result.Data as Payment;
            Assert.NotNull(expectedResult.Id);
        }

        [Fact]
        public async Task Should_NotCreatePaymentWithout_OrderID()
        {
            using var transaction = setup.dbContext.Database.BeginTransaction();
            
            // Given
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper, _logger.Object);
            AddPaymentDTO dto = new AddPaymentDTO{
                Amount = 1000,
                CurrencyCode = "NGN",
            };
            var result = await paymentRequest.CreatePayment(dto);
            Assert.Equal(false, result.IsSuccess);
        }

        [Fact]
        public async Task Should_NotCreatePaymentWithout_Amount()
        {
            using var transaction = setup.dbContext.Database.BeginTransaction();
            
            // Given
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper, _logger.Object);
            AddPaymentDTO dto = new AddPaymentDTO{
                OrderId = 1,
                CurrencyCode = "NGN",
            };
            var result = await paymentRequest.CreatePayment(dto);
            Assert.Equal(false, result.IsSuccess);
        }

        [Fact]
        public async Task Should_NotCreatePaymentWith_CurrencyCode_GreaterThan7()
        {
            using var transaction = setup.dbContext.Database.BeginTransaction();
            
            // Given
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper, _logger.Object);
            AddPaymentDTO dto = new AddPaymentDTO{
                OrderId = 1,
                Amount = 3500,
                CurrencyCode = "NGNNNNNN",
            };
            var result = await paymentRequest.CreatePayment(dto);
            Assert.Equal(false, result.IsSuccess);
        }

        [Fact]
        public async Task ShouldGetPaymentById_EnsureOrderObject_IsNotNull()
        {
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper, _logger.Object);
            
            var result = await paymentRequest.GetPaymentById(1);
            var expectedResult = result.Data as Payment;
            Assert.NotNull(expectedResult.Order);
        }
    }

}