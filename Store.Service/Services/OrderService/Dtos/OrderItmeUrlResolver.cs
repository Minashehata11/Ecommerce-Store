using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Service.Services.Dtos;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderItmeUrlResolver : IValueResolver<OrderList, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItmeUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderList source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrderd.PicturUrl))
            {
                return $"{_configuration["baseUrl"]}{source.ItemOrderd.PicturUrl}";
            }
            else
                return null;
        }
    }
}