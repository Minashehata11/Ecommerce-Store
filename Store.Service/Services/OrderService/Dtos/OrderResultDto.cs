using Store.Data.Entities.OrderEntities;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderResultDto
    {
        public int OrderId { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public AdressDto shippngAddress { get; set; } 
        public string deliveryMethodName { get; set; }
        public orderPaymentStatus orderPaymentStatus { get; set; } 
        public IReadOnlyList<OrderItemDto> orderItems { get; set; }
        public string? PaymentIntentId { get; set; }

        public decimal subtotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Total { get; set; }
        
        public string basketId { get; set; }    
       
    }
}
