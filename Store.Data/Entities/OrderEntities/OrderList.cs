using System.Security.Principal;

namespace Store.Data.Entities.OrderEntities
{
    public class OrderList:BaseEntity<Guid>
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public productItemOrderd ItemOrderd { get; set; }
        public Guid OrderId { get; set; }

    }
}