using System;
using BezCepay.Data.Enums;

namespace BezCepay.Data.Models
{
    public class Payment : ModelBase
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string CurrencyCode { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public Order Order { get; set; }
    }
}