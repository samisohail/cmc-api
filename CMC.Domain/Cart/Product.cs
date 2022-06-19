using System;
using System.Linq;

namespace CMC.Domain.Cart
{
    public class Product
    {
        public int ProductId { get; internal set; }
        public string Name { get; internal set; }
        public double UnitPrice { get; internal set; }
        public string Currency { get; internal set; }

        internal Product() { }

        public void SetPriceWithCurrency(double unitPrice, string currency)
        {
            this.Currency = currency;
            this.UnitPrice = unitPrice;
        }
    }
}
