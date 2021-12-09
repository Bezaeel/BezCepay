using System;

namespace BezCepay.Data.Models
{
    public class Order : ModelBase
    {
        public int Id { get; set; }
        public string ConsumerFullname { get; set; }
        public string ConsumerAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}