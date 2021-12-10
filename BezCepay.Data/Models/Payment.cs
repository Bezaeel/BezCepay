using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using BezCepay.Data.Enums;

namespace BezCepay.Data.Models
{
    public class Payment : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="amount is required and should be in base denomination e.g cent, kobo, penny")]
        public int? Amount { get; set; }
        public int OrderId { get; set; }

        [MaxLength(7, ErrorMessage ="length of currency code cannot be more than 7")]
        [Required(ErrorMessage ="currency code for amount is required")]
        public string CurrencyCode { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateAt { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}