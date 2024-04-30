using Store.Data.Entities;
using Store.Rebository.Specefication;
using Store.Rebository.Specefication.ProductSpecefication;
using Store.Service.Helper;
using Store.Service.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Products
{
    public interface IProductService
    {
       public Task<PaginatedResultDto<ProductViewDto>> GetAllProduct(SpecificationProductParameter input);
       public Task<ProductViewDto> GetProductById(int? id);
       public Task<IReadOnlyList<BrandOrTypeViewDto>> GetAllBrands();
       public Task<IReadOnlyList<BrandOrTypeViewDto>> GetAllTypes();

    }
}
