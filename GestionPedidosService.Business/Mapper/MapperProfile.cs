using AutoMapper;
using GestionPedidosService.Business.Extension;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Extensions;
using GestionPedidosService.Domain.Models;
using System.Linq;

namespace GestionPedidosService.Business.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<FeatureGarment, FeatureRead>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TypeFeature));

            /*CreateMap<OrderDetail, OrderRead>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GarmentId, opt => opt.MapFrom(src => src.Garment.Id))
                .ForMember(dest => dest.GarmentCode, opt => opt.MapFrom(src => src.Garment.CodeGarment))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Order.UserClientId))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Order.CodeOrder))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (src.Garment.FirstRangePrice + src.Garment.SecondRangePrice) / 2))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Order.OrderDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Order.OrderStatus.ToDescriptionString()));*/

            CreateMap<Order, OrderRead>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.OrderDetails.Aggregate(0.0, (sum, current) => sum + ((current.Garment.FirstRangePrice + current.Garment.SecondRangePrice) / 2))))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToDescriptionString()))
                .ForMember(dest => dest.AttendedBy, opt => opt.MapFrom(src => src.UserAtelier.User.NameUser + " " + src.UserAtelier.User.LastNameUser))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.UserClient));

            CreateMap<OrderDetail, OrderDetailMin>()
                .ForMember(dest => dest.CodeGarment, opt => opt.MapFrom(src => src.Garment.CodeGarment))
                .ForMember(dest => dest.OrderDetailStatus, opt => opt.MapFrom(src => src.OrderDetailStatus.ToDescriptionString()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (src.Garment.FirstRangePrice + src.Garment.SecondRangePrice) / 2));

            CreateMap<UserClient, UserClientMin>()
                .ForMember(dest => dest.NameClient, opt => opt.MapFrom(src => src.User.NameUser + " " + src.User.LastNameUser))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            /*CreateMap<OrderDetail, OrderDetailRead>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Order.CodeOrder))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Order.UserClientId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Order.OrderDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.GarmentCode, opt => opt.MapFrom(src => src.Garment.CodeGarment))
                .ForMember(dest => dest.AtelierId, opt => opt.MapFrom(src => src.Order.AtelierId))
                .ForMember(dest => dest.GarmentName, opt => opt.MapFrom(src => src.Garment.NameGarment))
                .ForMember(dest => dest.SelectedColor, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Order.OrderStatus.ToDescriptionString()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (src.Garment.FirstRangePrice + src.Garment.SecondRangePrice) / 2))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Garment.FeatureGarments));*/

            CreateMap<PatternDimension, PatternDimensionRead>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units));

            CreateMap<PatternGarment, PatternGarmentRead>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TypePattern))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImagePattern))
                .ForMember(dest => dest.ScaledStatus, opt => opt.MapFrom(src => src.ScaledStatus))
                .ForMember(dest => dest.Dimensions, opt => opt.MapFrom(src => src.PatternDimensions));
        }
    }
}