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
using BezCepay.Service.Features.OrderFlow;

namespace BezCepay.Test
{
    public class OrderControllerUnitTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<OrderRequest>> _logger;
        private readonly Mock<ILogger<OrderController>> _loggerController;
        static Setup setup;
        public OrderControllerUnitTests()
        {
            setup = new Setup();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Configs());
            });
            mapper = mockMapper.CreateMapper();
            _logger = new Mock<ILogger<OrderRequest>>();
            _loggerController = new Mock<ILogger<OrderController>>();
            
        }

        [Fact]
        public async Task GetAllPayments()
        {
            var repo = new OrderRepository<Order>(setup.dbContext);
            var serviceMock = new OrderRequest(repo, mapper,_logger.Object);
            var actionResult = new OrderController(serviceMock, mapper, _loggerController.Object);
            var expectedResult = await actionResult.list() as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, expectedResult.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnStatusCode200_CorrectInput()
        {
            var repo = new OrderRepository<Order>(setup.dbContext);
            var serviceMock = new OrderRequest(repo, mapper,_logger.Object);
            var actionResult = new OrderController(serviceMock, mapper, _loggerController.Object);
            var expectedResult = await actionResult.GetOrderById(1) as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, expectedResult.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnStatusCode404_NotfoundID()
        {
            var repo = new OrderRepository<Order>(setup.dbContext);
            var serviceMock = new OrderRequest(repo, mapper,_logger.Object);
            var actionResult = new OrderController(serviceMock, mapper, _loggerController.Object);
            var expectedResult = await actionResult.GetOrderById(100) as ObjectResult;
            Assert.Equal(StatusCodes.Status404NotFound, expectedResult.StatusCode);
        }
    }
}