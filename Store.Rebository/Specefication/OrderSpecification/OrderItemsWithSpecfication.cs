using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository.Specefication.OrderSpecification
{
    public class OrderItemsWithSpecification : SpeceficationBase<Order>
    {
        public OrderItemsWithSpecification(string buyerEmail) : 
            base(order=>order.BuyerEmail==buyerEmail)
        {
            AddInclude(order => order.orderItems);
            AddInclude(order => order.deliveryMethod);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderItemsWithSpecification(Guid id,string buyerEmail) :
           base(order => order.BuyerEmail == buyerEmail && order.Id==id)
        {
            AddInclude(order => order.orderItems);
            AddInclude(order => order.deliveryMethod);
        }
    }
}
