using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BezCepay.Service.Features.PaymentFlow;
using BezCepay.Service.Features.PaymentFlow.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BezCepay.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRequest _paymentRequest;
        private readonly IMapper _mapper;
        public PaymentController(IPaymentRequest paymentRequest, IMapper mapper)
        {
            _paymentRequest = paymentRequest;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> create(AddPaymentDTO payment)
        {
            if(!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
            var result = await _paymentRequest.CreatePayment(payment);
            if(result.Code == Service.Communication.ErrorCodes.Success){
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("invalid id");
            }
            var result = await _paymentRequest.GetPaymentById(id);
            if(result.Code == Service.Communication.ErrorCodes.Success){
                return Ok(result);
            } else if(result.Code == Service.Communication.ErrorCodes.Notfound){
                return NotFound(result);
            }
            return StatusCode(500, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> Payments()
        {
            var result = await _paymentRequest.GetAllPayments();
            if(result.Code == Service.Communication.ErrorCodes.Success){
                return Ok(result);
            }
            return StatusCode(500, result);
        }
    }
}