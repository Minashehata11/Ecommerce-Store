using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Rebository.Interfaces;
using Store.Rebository.Specefication.OrderSpecification;
using Store.Service.Services.BasketServices;
using Store.Service.Services.BasketServices.Dtos;
using Store.Service.Services.OrderService.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketServices _basketServices;
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration configuration,IUnitOfWork unitOfWork,IBasketServices basketServices,IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketServices = basketServices;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentforExitingOrder(CustomerBasketDto basket)
        {
            StripeConfiguration.ApiKey = _configuration["Strip:Secretkey"];
            if (basket == null)
                return null;
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId);
            var shippingPrice = deliveryMethods.Price;
            foreach (var basketItem in basket.BasketItems)
            {
                var product = await _unitOfWork.Repository<Data.Entities.Product, int>().GetByIdAsync(basketItem.ProductId);
                if (basketItem.Price != product.Price)
                    basketItem.Price = product.Price;
            }
            var services = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(basket.PaymentIntentId)) 
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100) + (long)shippingPrice),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };

                paymentIntent = await services.CreateAsync(options);    
                basket.PaymentIntentId=paymentIntent.Id;  
                basket.ClientSecret=paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100) + (long)shippingPrice),
                };
               await services.UpdateAsync(basket.PaymentIntentId,options);

            }

            await _basketServices.UpdateBasketAsync(basket);

            return basket;

        }

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentforNewOrder(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["Strip:Secretkey"];
            var basket = await _basketServices.GetBasketAsync(BasketId);
            if (basket == null)
                return null;
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId);
            var shippingPrice = deliveryMethods.Price;
            foreach (var basketItem in basket.BasketItems)
            {
                var product = await _unitOfWork.Repository<Data.Entities.Product, int>().GetByIdAsync(basketItem.ProductId);
                if (basketItem.Price != product.Price)
                    basketItem.Price = product.Price;
            }
            var services = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100) + (long)shippingPrice),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };

                paymentIntent = await services.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100) + (long)shippingPrice),
                };
                await services.UpdateAsync(basket.PaymentIntentId, options);

            }

            await _basketServices.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<OrderResultDto> UpdateOrderPaymentFailed(string PaymentIntendId)
        {
            var specs = new OrderWithPaymentIntentId(PaymentIntendId);
            var order=await _unitOfWork.Repository<Order,Guid>().GetByIdAsyncWithSpecification(specs);
            if (order == null)
                throw new Exception("Not Found Order");
            order.orderPaymentStatus = orderPaymentStatus.Failed;
            _unitOfWork.Repository<Order, Guid>().Update(order);
            await _unitOfWork.CommitAsync();
            var mappedOrder = _mapper.Map<OrderResultDto>(order);
            return mappedOrder;
        }

        public async Task<OrderResultDto> UpdateOrderPaymentSucceeded(string PaymentIntendId, string basketId)
        {
            var specs = new OrderWithPaymentIntentId(PaymentIntendId);
            var order = await _unitOfWork.Repository<Order, Guid>().GetByIdAsyncWithSpecification(specs);
            if (order == null)
                throw new Exception("Not Found Order");
            order.orderPaymentStatus = orderPaymentStatus.Recieve;
            _unitOfWork.Repository<Order, Guid>().Update(order);
            await _unitOfWork.CommitAsync();
            await  _basketServices.DeleteBasketAsync(basketId);    
            var mappedOrder = _mapper.Map<OrderResultDto>(order);
            mappedOrder.basketId = basketId;
            return mappedOrder;
        }
    }
}
