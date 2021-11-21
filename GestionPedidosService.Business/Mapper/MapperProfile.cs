using AutoMapper;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Models;

namespace GestionPedidosService.Business.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OrderDetail, OrderRead>()
                .ForMember(dest => dest.GarmentCode, opt => opt.MapFrom(src => src.Garment.CodeGarment))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Order.UserClientId))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Order.CodeOrder))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (src.Garment.FirstRangePrice + src.Garment.SecondRangePrice) / 2))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Order.OrderDate))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Order.OrderStatus));
        }
    }
}