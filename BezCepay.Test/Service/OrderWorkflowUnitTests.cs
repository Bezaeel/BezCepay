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

namespace BezCepay.Test 
{
    public class OrderWorkflowUnitTests
    {
        private readonly IMapper mapper;
        static Setup setup;

        public OrderWorkflowUnitTests()
        {
            setup = new Setup();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Configs());
            });
            mapper = mockMapper.CreateMapper();
        }


        [Fact]
        public async Task ShouldCreateOrder()
        {
            var repo = new OrderRepository<Order>(setup.dbContext);
            var orderRequest = new OrderRequest(repo, mapper);
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
            var orderRequest = new OrderRequest(repo, mapper);
            
            var result = await orderRequest.GetAllOrders();
            var expectedResult = result.Data as List<Order>;
            Assert.Equal(2, expectedResult.Count);
        }

        [Fact]
        public async Task ShouldGetOrderById()
        {
            var repo = new OrderRepository<Order>(setup.dbContext);
            var orderRequest = new OrderRequest(repo, mapper);
            
            var result = await orderRequest.GetOrderById(1);
            var expectedResult = result.Data as Order;
            Assert.NotNull(expectedResult);
        }
    }

}