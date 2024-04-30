using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.OrderService;
using Store.Service.Services.OrderService.Dtos;
using Stripe.Climate;
using System.Security.Claims;

namespace Store.Api.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderServices _orderService;

        public OrderController(IOrderServices orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrder(OrderDto input)
        {
            var order   =await _orderService.CreateOrderAsync(input);
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrderForUsers()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders= await _orderService.GetAllOrdersForUserAsync(email);
            return Ok(orders);  
        }

        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrderByIdAsync(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order=await _orderService.GetOrderByIdAsync(id, email);
            return Ok(order);
        }
        

        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetDelivery()
        {
            var delivery = await _orderService.GetAllDeliveryMethodsAsync();
            return Ok(delivery);
        }
      


    }
}
