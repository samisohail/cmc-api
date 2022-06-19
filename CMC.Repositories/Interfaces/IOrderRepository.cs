using System;
using System.Collections.Generic;
using System.Text;
using CMC.Models;
using CMC.Models.Order;
using CMC.Repositories.DbEntities;

namespace CMC.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Result<DbOrder> Create(CreateOrderRequest orderRequest);
    }
}
