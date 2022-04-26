using AutoMapper;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Extensions;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Models.FeatureGarments;
using GestionPedidosService.Domain.Models.Garments;
using GestionPedidosService.Domain.Models.Orders;
using GestionPedidosService.Domain.Utils;
using System;
using System.Collections.Generic;
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

            CreateMap<Order, OrderReadMobile>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.OrderDetails.Aggregate(0.0, (sum, current) => sum + ((current.Garment.FirstRangePrice + current.Garment.SecondRangePrice) / 2))))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToDescriptionString()))
                .ForMember(dest => dest.UserClientId, opt => opt.MapFrom(src => src.UserClient.Id))
                .ForMember(dest => dest.NameAtelier, opt => opt.MapFrom(src => src.Atelier.NameAtelier))
                .ForMember(dest => dest.AtelierAddress, opt => opt.MapFrom(src => $"{src.Atelier.Address}, {src.Atelier.District}"))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.OrderDetails));
            

            CreateMap<OrderDetail, OrderDetailMin>()
                .ForMember(dest => dest.CodeGarment, opt => opt.MapFrom(src => src.Garment.CodeGarment))
                .ForMember(dest => dest.OrderDetailStatus, opt => opt.MapFrom(src => src.OrderDetailStatus.ToDescriptionString()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (src.Garment.FirstRangePrice + src.Garment.SecondRangePrice) / 2));

            CreateMap<OrderDetail, OrderDetailReadMobile>()
                .ForMember(dest => dest.NameGarment, opt => opt.MapFrom(src => src.Garment.NameGarment))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (src.Garment.FirstRangePrice + src.Garment.SecondRangePrice) / 2))
                .ForMember(dest => dest.OrderDetailStatus, opt => opt.MapFrom(src => src.OrderDetailStatus.ToDescriptionString()))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Garment.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.images.ToString()))
                    .Select(f => f.Value)
                    .FirstOrDefault())
                );

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
                .ForMember(dest => dest.Fabrics, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.fabric.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.Occasions, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.occasion.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.images.ToString()))
                    .Select(f => f.Value))
                );

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
                .ForMember(dest => dest.ScaledStatus, opt => opt.MapFrom(src => src.ResizedStatus))
                .ForMember(dest => dest.Dimensions, opt => opt.MapFrom(src => src.PatternDimensions));

            CreateMap<Garment, GarmentMinWeb>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.images.ToString()))
                    .Select(f => f.Value)
                    .FirstOrDefault())
                )
                //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToDescriptionString()))
                .ForMember(dest => dest.AveragePrice, opt => opt.MapFrom(src => (src.FirstRangePrice + src.SecondRangePrice) / 2));

            CreateMap<IEnumerable<DictionaryType>, ConfigurationTypeRead>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src
                    .Where(d => d.GroupType.Equals(STypes.GarmentCategory)))
                )
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src
                    .Where(d => d.GroupType.Equals(STypes.OrderStatus)))
                )
                .ForMember(dest => dest.Fabrics, opt => opt.MapFrom(src => src
                    .Where(d => d.GroupType.Equals(STypes.Fabrics)))
                )
                .ForMember(dest => dest.Occasions, opt => opt.MapFrom(src => src
                    .Where(d => d.GroupType.Equals(STypes.Occasion)))
                );

            CreateMap<DictionaryType, TypeRead>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.KeyType))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ValueType))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<GarmentWrite, Garment>()
                .ForMember(dest => dest.FeatureGarments, opt => opt.MapFrom(src => src.Features));
                

            CreateMap<FeatureGarmentWrite, FeatureGarment>();

            CreateMap<Garment, GarmentReadWeb>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.GetValues(typeof(EGarmentCategories))
                        .Cast<EGarmentCategories>()
                        .FirstOrDefault(e => e.Equals((EGarmentCategories)src.Category))
                        .ToDescriptionString())
                )
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.color.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.Fabrics, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.fabric.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.Occasions, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.occasion.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.images.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.Patterns, opt => opt.MapFrom(src => src.PatternGarments.Select(p => p.ImagePattern)));

            CreateMap<Garment, GarmentMinMobile>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.images.ToString()))
                    .Select(f => f.Value)
                    .FirstOrDefault())
                )
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.FirstRangePrice))
                .ForMember(dest => dest.NameAtelier, opt => opt.MapFrom(src => src.Atelier.NameAtelier));

            CreateMap<Garment, GarmentReadMobile>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.GetValues(typeof(EGarmentCategories))
                        .Cast<EGarmentCategories>()
                        .FirstOrDefault(e => e.Equals((EGarmentCategories)src.Category))
                        .ToDescriptionString())
                )
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.color.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.Fabrics, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.fabric.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.Occasions, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.occasion.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.FeatureGarments
                    .Where(f => f.TypeFeature.Equals(EGarmentFeatures.images.ToString()))
                    .Select(f => f.Value))
                )
                .ForMember(dest => dest.NameAtelier, opt => opt.MapFrom(src => src.Atelier.NameAtelier))
                .ForMember(dest => dest.AtelierAddress, opt => opt.MapFrom(src => $"{src.Atelier.Address}, {src.Atelier.District}"));

            CreateMap<OrderCreate, Order>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => EOrderStatus.unattended))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.Details));

            CreateMap<OrderDetailCreate, OrderDetail>()
                .ForMember(dest => dest.OrderDetailStatus, opt => opt.MapFrom(src => EOrderStatus.unattended));

        }
    }
}