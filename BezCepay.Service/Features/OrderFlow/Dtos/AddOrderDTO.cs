using System.ComponentModel.DataAnnotations;

namespace BezCepay.Service.Features.OrderFlow.Dtos
{
    public class AddOrderDTO
    {
        [Required]
        public string ConsumerFullName { get; set; }
        public string ConsumerAddress { get; set; }
    }
}
