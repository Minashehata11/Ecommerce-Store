using AutoMapper;
using Store.Data.Entities.IdentityEntities;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Address,AdressDto>().ReverseMap(); 
            CreateMap<AdressDto,ShippngAddress>().ReverseMap();

            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.deliveryMethodName, options => options.MapFrom(src => src.deliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.deliveryMethod.Price))
                .ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.deliveryMethod.Price));

            CreateMap<OrderList, OrderItemDto>()
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.ItemOrderd.ProductName))
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.ItemOrderd.ProductItemId))
                .ForMember(dest => dest.PicturUrl, options => options.MapFrom(src => src.ItemOrderd.PicturUrl))
                .ForMember(dest => dest.PicturUrl, options => options.MapFrom<OrderItmeUrlResolver>());
                

        }
    }
}
