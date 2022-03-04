using AutoMapper;
using GestionPedidosService.Business.Extension;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Extensions;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Utils;
using System.Linq;

namespace GestionPedidosService.Business.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
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

            CreateMap<OrderDetail, OrderDetailRead>()
                .ForMember(dest => dest.CodeOrder, opt => opt.MapFrom(src => src.Order.CodeOrder))
                .ForMember(dest => dest.OrderDetailStatus, opt => opt.MapFrom(src => src.OrderDetailStatus.ToDescriptionString()))
                .ForMember(dest => dest.AttendedBy, opt => opt.MapFrom(src => src.Order.UserAtelier.User.NameUser + " " + src.Order.UserAtelier.User.LastNameUser))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.Order.OrderDate))
                .ForMember(dest => dest.Garment, opt => opt.MapFrom(src => src.Garment))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Order.UserClient))
                .ForPath(dest => dest.Garment.Color, opt => opt.MapFrom(src => src.Color))
                .ForPath(dest => dest.Garment.Quantity, opt => opt.MapFrom(src => src.Quantity));


            CreateMap<Garment, CustomGarmentRead>()
                .ForMember(dest => dest.EstimatedPrice, opt => opt.MapFrom(src => (src.FirstRangePrice + src.SecondRangePrice) / 2))
                .ForMember(dest => dest.Fabrics, opt => opt.MapFrom(src => src.FeatureGarments.Where(f => f.TypeFeature.Equals(EFeatureGarments.Fabric.ToDescriptionString())).Select(f => f.Value)))
                .ForMember(dest => dest.Occasions, opt => opt.MapFrom(src => src.FeatureGarments.Where(f => f.TypeFeature.Equals(EFeatureGarments.Occasion.ToDescriptionString())).Select(f => f.Value)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.FeatureGarments.Where(f => f.TypeFeature.Equals(EFeatureGarments.Images.ToDescriptionString())).Select(f => f.Value)));

            CreateMap<UserClient, UserClientMin>()
                .ForMember(dest => dest.NameClient, opt => opt.MapFrom(src => src.User.NameUser + " " + src.User.LastNameUser))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<FeatureGarment, FeatureGarmentMin>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TypeFeature));

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