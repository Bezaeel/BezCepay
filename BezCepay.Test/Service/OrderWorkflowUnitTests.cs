using System.Collections.Generic;
using AutoMapper;
using BezCepay.Data.Models;
using BezCepay.Service.Features.OrderFlow;
using Moq;
using Xunit;
using BezCepay.Service.Features.OrderFlow.Dtos;
using BezCepay.Data.Repositories;
using System.Threading.Tasks;
using BezCepay.Service.Mappings;
using Microsoft.Extensions.Logging;

namespace BezCepay.Test
{
    public class OrderWorkflowUnitTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<OrderRequest>> _logger;
        static Setup setup;

        public OrderWorkflowUnitTests()
        {
            setup = new Setup();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Configs());
            });
            mapper = mockMapper.CreateMapper();
            _logger = new Mock<ILogger<OrderRequest>>();
        }


        [Fact]
        public async Task ShouldCreateOrder()
        {
            // var logger = new Mock<ILogger<OrderRequest>>();
            var repo = new OrderRepository<Order>(setup.dbContext);
            var orderRequest = new OrderRequest(repo, mapper, _logger.Object);
            AddOrderDTO dto = new AddOrderDTO{
                ConsumerAddress = "Lagos",
                ConsumerFullName = "Talabi"
            };
            var result = await orderRequest.CreateOrder(dto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetAllOrders()
        {
            var repo = new OrderRepository<Order>(setup.dbContext);
            var orderRequest = new OrderRequest(repo, mapper, _logger.Object);
            
            var result = await orderRequest.GetAllOrders();
            var expectedResult = result.Data as List<Order>;
            Assert.Equal(2, expectedResult.Count);
        }

        [Fact]
        public async Task ShouldGetOrderById()
        {
            var repo = new OrderRepository<Order>(setup.dbContext);
            var orderRequest = new OrderRequest(repo, mapper, _logger.Object);
            
            var result = await orderRequest.GetOrderById(1);
            var expectedResult = result.Data as Order;
            Assert.NotNull(expectedResult);
        }
    }

}