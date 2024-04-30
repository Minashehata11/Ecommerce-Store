using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository.Specefication.ProductSpecefication
{
    public class SpecefiationEvaluator<TEntity,TKey>where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> Inputquery,ISpecefication<TEntity> specs) 
        {
            var query = Inputquery;
            if (specs.Criteria != null)
                query = query.Where(specs.Criteria); 
            if (specs.Order != null)
                query = query.OrderBy(specs.Order);
            if (specs.OrderByDescending != null)
                query = query.OrderByDescending(specs.OrderByDescending);
            if(specs.IsPaginated)
                query = query.Skip(specs.Skip).Take(specs.Take);
            query =specs.Includes.Aggregate(query,(current,includeExprision)=>current.Include(includeExprision));

            return query;
        }
    }
}
