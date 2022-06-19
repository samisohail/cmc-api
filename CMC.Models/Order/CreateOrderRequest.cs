using System;
using System.Collections.Generic;
using System.Text;

namespace CMC.Models.Order
{
    public class CreateOrderRequest
    {
        public IEnumerable<CartItemDto> CartItems { get; set; }
        public string Address { get; set; }
        public int CustomerId { get; set; }
    }
}
