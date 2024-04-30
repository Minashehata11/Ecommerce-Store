using AutoMapper;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Dtos.ProfilesMapper
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewDto>()
                .ForMember(dest => dest.BrandName, option => option.MapFrom(src => src.productBrand.Name)).
                 ForMember(dest => dest.TypeName, option => option.MapFrom(src => src.ProductType.Name))
                 .ForMember(dest => dest.PictureUrl, option => option.MapFrom<ProductUrlResolver>());

            CreateMap<ProductBrand, BrandOrTypeViewDto>();
            CreateMap<ProductType, BrandOrTypeViewDto>();
        }
    }
}
