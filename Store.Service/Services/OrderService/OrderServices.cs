using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Rebository.Interfaces;
using Store.Rebository.Specefication.OrderSpecification;
using Store.Service.Services.BasketServices;
using Store.Service.Services.OrderService.Dtos;
using Store.Service.Services.PaymentServices;

namespace Store.Service.Services.OrderService
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketServices _basket;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderServices(
            IUnitOfWork unitOfWork,
            IBasketServices basket,
            IMapper mapper,
            IPaymentService paymentService
            )
        {
             _unitOfWork = unitOfWork;
             _basket = basket;
             _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
        {
            var basket =  await _basket.GetBasketAsync(input.BasketId);
            if (basket == null)
                throw new Exception("Not Found Basket");
            var orderItme = new List<OrderItemDto>();
            foreach (var basketItem in basket.BasketItems) 
            {
                var productItem= await _unitOfWork.Repository<Product,int>().GetByIdAsync(basketItem.ProductId);
                if (productItem == null)
                    throw new Exception("Not Found Product");
                var itemOrderd = new productItemOrderd()
                {
                    ProductName = productItem.Name,
                    PicturUrl = productItem.PictureUrl,
                    ProductItemId = productItem.Id
                };
                var orderItem = new OrderList
                {
                    Price= productItem.Price,
                    Quantity=basketItem.Quantity,
                    ItemOrderd= itemOrderd,
                };
                 var mappedOrderItem=_mapper.Map<OrderItemDto>(orderItem);  
                  orderItme.Add(mappedOrderItem);   
            }
            var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod,int>().GetByIdAsync(input.DeliveryMethodId);
            if (deliveryMethod == null) throw new Exception("");
            //calculate SubTotal
             var subTotal=orderItme.Sum(item=>item.Quantity*item.Price);

            var specs = new OrderWithPaymentIntentId(basket.Id);
            var exitingOrder = await _unitOfWork.Repository<Order, Guid>().GetByIdAsyncWithSpecification(specs);
            if(exitingOrder != null)
            {
                _unitOfWork.Repository<Order,Guid>().Delete(exitingOrder);
                await _paymentService.CreateOrUpdatePaymentforExitingOrder(basket);

            }
            else
            {
                await _paymentService.CreateOrUpdatePaymentforNewOrder(basket.Id);

            }
            //create order
            var mappedShippingAdress = _mapper.Map<ShippngAddress>(input.ShippingAdress);
            var mappedOrderItems = _mapper.Map<List<OrderList>>(orderItme);
            var order = new Order
            {
                DeliveryMethodId=deliveryMethod.Id,
                shippngAddress=mappedShippingAdress,
                BuyerEmail=input.BuyerEmail,
                orderItems= mappedOrderItems,
                subtotal=subTotal,

            };
            await _unitOfWork.Repository<Order, Guid>().AddAsync(order);
            await _unitOfWork.CommitAsync();    

             var mappedOrder=_mapper.Map<OrderResultDto>(order); 
            
             mappedOrder.basketId=basket.Id;
             return mappedOrder;
        }



        public Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        => _unitOfWork.Repository<DeliveryMethod,int>().GetAllAsync();  

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            var specs =  new OrderItemsWithSpecification(BuyerEmail);
            var order= await _unitOfWork.Repository<Order,Guid>().GetAllAsyncWithSpecification(specs);
            var mappedOrder=_mapper.Map<List<OrderResultDto>>(order);

            return mappedOrder;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id, string BuyerEmail)
        {
            var specs = new OrderItemsWithSpecification(id,BuyerEmail);
            var order = await _unitOfWork.Repository<Order, Guid>().GetByIdAsyncWithSpecification(specs);
            var mappedOrder= _mapper.Map<OrderResultDto>(order);

            return mappedOrder;

        }
    }
}
