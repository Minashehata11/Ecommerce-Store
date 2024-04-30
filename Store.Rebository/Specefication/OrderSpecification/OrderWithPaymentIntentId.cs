using StackExchange.Redis;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository.Specefication.OrderSpecification
{
    public class OrderWithPaymentIntentId : SpeceficationBase<Data.Entities.OrderEntities.Order>
    {
        public OrderWithPaymentIntentId(string intentId) :
             base(order => order.PaymentIntentId == intentId)
        {
          
        }

    }
}
