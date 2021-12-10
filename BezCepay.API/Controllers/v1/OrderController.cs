using System.Threading.Tasks;
using AutoMapper;
using BezCepay.Service.Features.OrderFlow;
using BezCepay.Service.Features.OrderFlow.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BezCepay.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRequest _orderRequest;
        private readonly IMapper _mapper;
        public OrderController(IOrderRequest orderRequest, IMapper mapper)
        {
            _orderRequest = orderRequest;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult list()
        {
            var result = _orderRequest.GetAllOrders();
            return Ok(result);
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> create(AddOrderDTO order)
        {
            var result = await _orderRequest.CreateOrder(order);
            return Ok(result);
        }
    }
}