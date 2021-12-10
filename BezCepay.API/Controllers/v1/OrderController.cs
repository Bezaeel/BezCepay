using System.Threading.Tasks;
using AutoMapper;
using BezCepay.Service.Features.OrderFlow;
using BezCepay.Service.Features.OrderFlow.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BezCepay.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRequest _orderRequest;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderRequest orderRequest, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderRequest = orderRequest;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> list()
        {
            var result = await _orderRequest.GetAllOrders();
            if(result.Code == Service.Communication.ErrorCodes.Success){
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> create(AddOrderDTO order)
        {
            var result = await _orderRequest.CreateOrder(order);
            if(result.Code == Service.Communication.ErrorCodes.Success){
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpGet("find/:id")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderRequest.GetOrderById(id);
            if(result.Code == Service.Communication.ErrorCodes.Success){
                return Ok(result);
            } else if(result.Code == Service.Communication.ErrorCodes.Notfound){
                return NotFound(result);
            }
            return StatusCode(500, result);
        }
    }
}