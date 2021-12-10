using System.ComponentModel.DataAnnotations;

namespace BezCepay.Service.Features.PaymentFlow.Dtos
{
    public class AddPaymentDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required(ErrorMessage ="amount is required and should be in base denomination e.g cent, kobo, penny")]
        public int? Amount { get; set; }
        [MaxLength(7, ErrorMessage ="length of currency code cannot be more than 7")]
        [Required(ErrorMessage ="currency code for amount is required")]
        public string CurrencyCode { get; set; }
    }
}
