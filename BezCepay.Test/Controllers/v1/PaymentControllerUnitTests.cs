using System.Threading.Tasks;
using AutoMapper;
using BezCepay.API.Controllers.v1;
using BezCepay.Data.Models;
using BezCepay.Data.Repositories;
using BezCepay.Service.Features.PaymentFlow;
using BezCepay.Service.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BezCepay.Test
{
    public class PaymentControllerUnitTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<PaymentRequest>> _logger;
        static Setup setup;
        public PaymentControllerUnitTests()
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
        public async Task GetAllPayments()
        {
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var serviceMock = new PaymentRequest(repo, mapper,_logger.Object);
            var actionResult = new PaymentController(serviceMock, mapper);
            var expectedResult = await actionResult.Payments() as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, expectedResult.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnStatusCode200_CorrectInput()
        {
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var serviceMock = new PaymentRequest(repo, mapper,_logger.Object);
            var actionResult = new PaymentController(serviceMock, mapper);
            var expectedResult = await actionResult.GetPaymentById(100) as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, expectedResult.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnStatusCode404_NotfoundID()
        {
            var repo = new PaymentRepository<Payment>(setup.dbContext);
            var serviceMock = new PaymentRequest(repo, mapper,_logger.Object);
            var actionResult = new PaymentController(serviceMock, mapper);
            var expectedResult = await actionResult.GetPaymentById(100) as ObjectResult;
            Assert.Equal(StatusCodes.Status404NotFound, expectedResult.StatusCode);
        }
    }
}