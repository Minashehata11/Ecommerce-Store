using AutoMapper;
using Store.Data.Entities;
using Store.Rebository.Interfaces;
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
    public class ProductServices : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandOrTypeViewDto>> GetAllBrands()
        {

           var brands= await _unitOfWork.Repository<ProductBrand,int>().GetAllAsync();
           var brandDto=_mapper.Map<IReadOnlyList<BrandOrTypeViewDto>>(brands);
            return  brandDto;
        }
        

        public async Task<PaginatedResultDto<ProductViewDto>> GetAllProduct(SpecificationProductParameter input)
        {
            var specs = new ProductWithSpecification(input);
            var countSpecs = new ProductWithCountAndFilterSpecification(input);
            var counts = await _unitOfWork.Repository<Product, int>().CountSpeceficationAsync(countSpecs);
            var Product = await _unitOfWork.Repository<Product, int>().GetAllAsyncWithSpecification(specs);
            var productDto=_mapper.Map<IReadOnlyList<ProductViewDto>>(Product);
            return new PaginatedResultDto<ProductViewDto>(input.PageIndex,input.PageSize, counts, productDto);
        }

        public async Task<IReadOnlyList<BrandOrTypeViewDto>> GetAllTypes()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            var brandsDto = _mapper.Map<IReadOnlyList<BrandOrTypeViewDto>>(brands);
            return brandsDto;
        }

        public async Task<ProductViewDto> GetProductById(int? id)
        {

            if(id is null) throw new ArgumentNullException(nameof(id));
            var specs = new ProductWithSpecification(id);
            var product = await _unitOfWork.Repository<Product, int>().GetByIdAsyncWithSpecification(specs);
            var productDto=_mapper.Map<ProductViewDto>(product);
            return productDto;
        }
    }
}
