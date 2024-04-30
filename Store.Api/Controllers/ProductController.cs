using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Rebository.Specefication.ProductSpecefication;
using Store.Service.Helper;
using Store.Service.Services.Dtos;
using Store.Service.Services.Products;

namespace Store.Api.Controllers
{
    [Authorize]
    public class ProductController :BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<PaginatedResultDto<ProductViewDto>>> GetAllProduct([FromQuery]SpecificationProductParameter input)
        => Ok(await _productService.GetAllProduct(input));

        [HttpGet]
        [Cache(80)]
        public async Task<ActionResult<IReadOnlyList<BrandOrTypeViewDto>>> GetAllBrands()
       => Ok(await _productService.GetAllBrands());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandOrTypeViewDto>>> GetAllTypes()
      => Ok(await _productService.GetAllTypes());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandOrTypeViewDto>>> GetProductById(int id)
      => Ok(await _productService.GetProductById(id));
    }
}
