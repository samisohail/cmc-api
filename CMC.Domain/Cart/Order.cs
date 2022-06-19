using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMC.Domain.Cart
{
    public class Order
    {
        public int OrderId { get; internal set; }
        public DateTime OrderDateTimeUtc { get; internal set; }
        public IReadOnlyCollection<Product> Products { get; internal set; }
        private double TotalCost => Products.Sum(p => p.UnitPrice);
        public int ShippingCost { get; internal set; }
    
        internal Order() { }
    }
}
