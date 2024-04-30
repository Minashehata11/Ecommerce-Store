using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketServices;
using Store.Service.Services.BasketServices.Dtos;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketServices _basketServices;

        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
            => Ok(await _basketServices.GetBasketAsync(id));
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket( CustomerBasketDto basket)
            => Ok(await _basketServices.UpdateBasketAsync(basket));
        [HttpDelete]
        public async Task<ActionResult<CustomerBasketDto>> DeleteBasket(string id)
            => Ok(await _basketServices.DeleteBasketAsync(id));

    }
}
