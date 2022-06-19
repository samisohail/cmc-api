using System;
using System.Collections.Generic;
using System.Text;

namespace CMC.Models.Order
{
    public class OrderCostResponse
    {
        public double ProductsTotal { get; set; }
        public double Shipping { get; set; }
        public string Currency { get; set; }
    }
}
