using AutoMapper;
using BezCepay.Data.Models;
using BezCepay.Service.Features.OrderFlow.Dtos;
using BezCepay.Service.Features.PaymentFlow.Dtos;

namespace BezCepay.Service.Mappings
{
    public class Configs : Profile
    {
        public Configs()
        {
            CreateMap<Payment, AddPaymentDTO>().ReverseMap();
            // CreateMap<AddPaymentDTO, Payment>();
            CreateMap<AddOrderDTO, Order>();
            CreateMap<Order, AddOrderDTO>();
        }
       
    }
}
