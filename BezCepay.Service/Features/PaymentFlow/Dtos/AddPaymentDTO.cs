using System.ComponentModel.DataAnnotations;

namespace BezCepay.Service.Features.PaymentFlow.Dtos
{
    public class AddPaymentDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
