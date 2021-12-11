using System.ComponentModel.DataAnnotations;

namespace BezCepay.Service.Features.PaymentFlow.Dtos
{
    public class AddPaymentDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required(ErrorMessage ="amount is required and should be in base denomination e.g cent, kobo, penny")]
        [Range(typeof(int), "100","9999999999", ErrorMessage = "minimum amout is 100 cents => 1 USD, 100 kobo => 1 Naira, etc" )]
        public int? Amount { get; set; }
        [MaxLength(7, ErrorMessage ="length of currency code cannot be more than 7")]
        [Required(ErrorMessage ="currency code for amount is required")]
        public string CurrencyCode { get; set; }
    }
}
