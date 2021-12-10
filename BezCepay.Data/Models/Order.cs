using System;
using System.ComponentModel.DataAnnotations;

namespace BezCepay.Data.Models
{
    public class Order : ModelBase
    {
        [Key]
        public int Id { get; set; }
        public string ConsumerFullname { get; set; }
        public string ConsumerAddress { get; set; }
        public Payment Payment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}