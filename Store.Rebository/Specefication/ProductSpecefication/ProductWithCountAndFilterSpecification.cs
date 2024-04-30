using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository.Specefication.ProductSpecefication
{
    public class ProductWithCountAndFilterSpecification : SpeceficationBase<Product>
    {
        public ProductWithCountAndFilterSpecification(SpecificationProductParameter specs) :
            base
            (product => (!specs.BrandId.HasValue || product.productBrandId == specs.BrandId.Value)
              && (!specs.TypeId.HasValue || product.productTypeId == specs.TypeId.Value)
               && (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search))
                )
        {
        }
    }
}
