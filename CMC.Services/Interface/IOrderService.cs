using System;
using System.Collections.Generic;
using System.Text;
using CMC.Models;
using CMC.Models.Order;

namespace CMC.Services.Interface
{
    public interface IOrderService
    {
        public Result<bool> ValidateOrderItems(IEnumerable<CartItemDto> cartItems);
        public Result<CreateOrderResponse> Create(CreateOrderRequest order);
    }
}
