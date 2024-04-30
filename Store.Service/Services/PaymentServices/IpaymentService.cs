using Store.Service.Services.BasketServices.Dtos;
using Store.Service.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentforExitingOrder(CustomerBasketDto input);
        Task<CustomerBasketDto> CreateOrUpdatePaymentforNewOrder(string BasketId);
        Task<OrderResultDto> UpdateOrderPaymentSucceeded (string PaymentIntendId,string basketId);
        Task<OrderResultDto> UpdateOrderPaymentFailed (string PaymentIntendId);
    }
}
