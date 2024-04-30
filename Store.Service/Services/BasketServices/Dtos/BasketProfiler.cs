using AutoMapper;
using Store.Rebository.BasketRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketServices.Dtos
{
    public class BasketProfiler:Profile
    {
        public BasketProfiler()
        {
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        }
    }
}
