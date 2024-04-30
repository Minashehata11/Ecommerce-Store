using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository.Specefication
{
    public class SpeceficationBase<T> : ISpecefication<T>
    {
        public SpeceficationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> Order { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take  { get; private set; }

        public int Skip  { get; private set; }

        public bool IsPaginated  { get; private set; }

         protected void AddInclude(Expression<Func<T, object>> IncludeExpression)
            =>Includes.Add(IncludeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> order)
             => Order = order;
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
             => OrderByDescending = orderByDescending;
        protected void AddPaginated(int take,int skip)
        {
            Take= take;
            Skip= skip;
            IsPaginated = true;
        }
    }
}
