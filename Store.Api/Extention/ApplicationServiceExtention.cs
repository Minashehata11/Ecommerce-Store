using Store.Rebository.BasketRepository;
using Store.Rebository.Interfaces;
using Store.Rebository.Repository;
using Store.Service.Services.BasketServices;
using Store.Service.Services.BasketServices.Dtos;
using Store.Service.Services.CacheServices;
using Store.Service.Services.Dtos.ProfilesMapper;
using Store.Service.Services.OrderService;
using Store.Service.Services.PaymentServices;
using Store.Service.Services.Products;
using Store.Service.Services.TokenServices;
using Store.Service.UserServices;

namespace Store.Api.Extention
{
    public  static class ApplicationServiceExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductServices>();
            services.AddScoped<IBasketServices, BasketServices>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfiler));
           

            return services;
        }
    }
}
