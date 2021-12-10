using System.Diagnostics;
using System.Collections.Generic;
using AutoMapper;
using BezCepay.Data.Models;
using BezCepay.Service.Features.OrderFlow;
using Moq;
using Xunit;
using System;
using BezCepay.Data.IRepositories;
using BezCepay.Service.Features.OrderFlow.Dtos;
using BezCepay.Data;
using Microsoft.EntityFrameworkCore;
using BezCepay.Data.Repositories;
using System.Threading.Tasks;
using BezCepay.Service.Mappings;
using BezCepay.Service.Features.PaymentFlow;
using BezCepay.Service.Features.PaymentFlow.Dtos;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BezCepay.Test.Service 
{
    public class PaymentWorkflowUnitTests
    {
        private readonly IMapper mapper;
        
        static Setup setup;

        public PaymentWorkflowUnitTests()
        {
            setup = new Setup();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Configs());
            });
            mapper = mockMapper.CreateMapper();
        }


        [Fact]
        public async Task ShouldCreatePayment()
        {
            using var transaction = setup.dbContext.Database.BeginTransaction();
            
            // Given
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper);
            AddPaymentDTO dto = new AddPaymentDTO{
                OrderId = 1,
                Amount = 1000,
                CurrencyCode = "NGN",
            };
            var result = await paymentRequest.CreatePayment(dto);
            Assert.Equal(true, result.IsSuccess);
        }

        [Fact]
        public async Task ShouldGetAllPayments()
        {
            // Given
            var repo = new PaymentRepository<Order>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper);
            
            var result = await paymentRequest.GetAllPayments();
            var expectedResult = result.Data as List<Payment>;
            Assert.Equal(2, expectedResult.Count);
        }

        [Fact]
        public async Task ShouldGetPaymentById()
        {
            var repo = new PaymentRepository<Order>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper);
            
            var result = await paymentRequest.GetPaymentById(1);
            var expectedResult = result.Data as Payment;
            Assert.NotNull(expectedResult);
        }

        [Fact]
        public async Task Should_NotCreatePaymentWithout_OrderID()
        {
            using var transaction = setup.dbContext.Database.BeginTransaction();
            
            // Given
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var paymentRequest = new PaymentRequest(repo, mapper);
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
            var paymentRequest = new PaymentRequest(repo, mapper);
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
            var paymentRequest = new PaymentRequest(repo, mapper);
            AddPaymentDTO dto = new AddPaymentDTO{
                OrderId = 1,
                Amount = 3500,
                CurrencyCode = "NGNNNNNN",
            };
            var result = await paymentRequest.CreatePayment(dto);
            Assert.Equal(false, result.IsSuccess);
        }

        


        
    }

}