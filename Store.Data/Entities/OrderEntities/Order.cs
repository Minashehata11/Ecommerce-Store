using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.OrderEntities
{
    public enum orderPaymentStatus
    {
        Pending,
        Recieve,
        Failed
    }
    public class Order:BaseEntity<Guid>
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public ShippngAddress shippngAddress { get; set; }
        public DeliveryMethod? deliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public orderPaymentStatus orderPaymentStatus { get; set; }=orderPaymentStatus.Pending;
        public IReadOnlyList<OrderList> orderItems { get; set; }

        public string? PaymentIntentId { get; set; }
        public decimal subtotal { get; set; }

        public decimal GetTotal()
            => subtotal + deliveryMethod.Price;
    }
}
