using System;
using System.Collections.Generic;
using System.Text;

namespace CMC.Repositories.DbEntities
{
    public class DbOrder
    {
        public int OrderId { get; set; }
        public string OrderRefNumber { get; set; }
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; }
    }
}
