using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository.Specefication.ProductSpecefication
{
    public class ProductWithSpecification : SpeceficationBase<Product>
    {
        public ProductWithSpecification(SpecificationProductParameter specs) :
            base
            ( product=>(!specs.BrandId.HasValue || product.productBrandId==specs.BrandId.Value)
              &&         (!specs.TypeId.HasValue  || product.productTypeId==specs.TypeId.Value)
            &&      (string.IsNullOrEmpty(specs.Search)||product.Name.Trim().ToLower().Contains(specs.Search)) )
                
        {
            AddInclude(x => x.productBrand);
            AddInclude(x => x.ProductType);
            AddOrderBy(x => x.Name);
            AddPaginated(specs.PageSize, (specs.PageIndex - 1) * specs.PageSize);
            if(!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                            break;
                    case "priceDesc":
                        AddOrderByDescending(x=>x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductWithSpecification(int? id) :
           base
           (product =>product.Id==id)
               
        {
            AddInclude(x => x.productBrand);
            AddInclude(x => x.ProductType);
        }
    }
}
