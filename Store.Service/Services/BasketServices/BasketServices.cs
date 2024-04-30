using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Store.Rebository.BasketRepository;
using Store.Rebository.BasketRepository.Models;
using Store.Service.Services.BasketServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketServices
{
    public class BasketServices : IBasketServices
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;

        public BasketServices(IMapper mapper,IBasketRepository basketRepository)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
        }
        public Task<bool> DeleteBasketAsync(string basketId)
          => _basketRepository.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var Basket= await _basketRepository.GetBasketAsync(basketId);
            if(Basket == null)
                return new CustomerBasketDto();
            var BaskedMapped=_mapper.Map<CustomerBasketDto>(Basket);
            return BaskedMapped;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
        {
            var CustomerBasket = _mapper.Map<CustomerBasket>(basket);
            var UpdatedBasked = await _basketRepository.UpdateBasketAsync(CustomerBasket);
            var MappedCustomerBasket = _mapper.Map<CustomerBasketDto>(UpdatedBasked);
            return MappedCustomerBasket;    
        }
    }
}
