using System;
using System.Collections.Generic;
using System.Linq;
using CMC.Models;
using CMC.Models.Order;
using CMC.Repositories.DbEntities;
using CMC.Repositories.Interfaces;

namespace CMC.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Result<DbOrder> Create(CreateOrderRequest orderRequest)
        {
            var orderId = Orders.Count() + 1;
            var dbOrder = new DbOrder
            {
                OrderId = orderId,
                CustomerId = orderRequest.CustomerId,
                OrderRefNumber = Guid.NewGuid().ToString(),
                ShippingAddress = orderRequest.Address
            };
            Orders.ToList().Add(dbOrder);

            // have to save cart products in another table e.g. OrderItems but that's not scope of this
            // coding challenge at the moment

            return Result.OK(dbOrder);
        }

        private IEnumerable<DbOrder> Orders = new List<DbOrder>()
        {
            new DbOrder
            {
                OrderId = 1, OrderRefNumber = Guid.NewGuid().ToString(), CustomerId = 1,
                ShippingAddress = "U 1, 63 Ferguson Avenue Wiley Park, NSW, Australia"
            },
            new DbOrder
            {
                OrderId = 2, OrderRefNumber = Guid.NewGuid().ToString(), CustomerId = 2,
                ShippingAddress = "U 1, 63 Ferguson Avenue Wiley Park, NSW, Australia"
            }
        };
    }
}
