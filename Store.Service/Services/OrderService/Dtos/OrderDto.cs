﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string BuyerEmail { get; set;}
        public int? DeliveryMethodId { get; set;}

        public AdressDto ShippingAdress { get; set; }
    }
}
