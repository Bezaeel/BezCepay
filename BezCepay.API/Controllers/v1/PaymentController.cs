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
            var result = await _paymentRequest.CreatePayment(payment);
            return Ok(result);
        }

        [HttpGet("all")]
        public IActionResult Payments()
        {
            return Ok("Ask Talabi..");
        }
    }
}