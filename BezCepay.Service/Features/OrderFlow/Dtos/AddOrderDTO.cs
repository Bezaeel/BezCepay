using System.ComponentModel.DataAnnotations;

namespace BezCepay.Service.Features.OrderFlow.Dtos
{
    public class AddOrderDTO
    {
        [Required(ErrorMessage ="fullname is required")]
        public string ConsumerFullName { get; set; }
        [Required(ErrorMessage ="address is required")]
        public string ConsumerAddress { get; set; }
    }
}
