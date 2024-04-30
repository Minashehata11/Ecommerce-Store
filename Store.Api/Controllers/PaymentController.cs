using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketServices.Dtos;
using Store.Service.Services.OrderService.Dtos;
using Store.Service.Services.PaymentServices;
using Stripe;

namespace Store.Api.Controllers
{
  
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private const string endpointSecret = "whsec_e380b303af894faca5623a3e0e71caeb5977fab05a2c81e11d5e36a4ce472bb7";

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentforExitingOrder(CustomerBasketDto Basket)
        => await _paymentService.CreateOrUpdatePaymentforExitingOrder(Basket);
       [HttpPost("basketId")]
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentforNewOrder(string BasketId)
        => await _paymentService.CreateOrUpdatePaymentforNewOrder(BasketId);

        //[Route("webhook")]
        //[ApiController]
        //public class WebhookController : Controller
        //{

            // This is your Stripe CLI webhook secret for testing your endpoint locally.
           // const string endpointSecret = "whsec_e380b303af894faca5623a3e0e71caeb5977fab05a2c81e11d5e36a4ce472bb7";

            [HttpPost("webhook")]
            public async Task<IActionResult> Index()
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                try
                {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);
                PaymentIntent paymentIntent;
                OrderResultDto order;
                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    order = await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);

                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                   // order = await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id,basketId);
                }
                
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
                }
                catch (StripeException e)
                {
                    return BadRequest();
                }
            }
        }
        
}
