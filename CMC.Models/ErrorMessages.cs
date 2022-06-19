using System;
using System.Collections.Generic;
using System.Text;

namespace CMC.Models
{
    public class ErrorMessages
    {
        public const string NoProductFound = "Product not found";
        public const string InvalidCurrency = "An invalid currency provided";
        public const string InvalidProduct = "An invalid product found";
        public const string MultipleCurrenciesInCart = "Multiple currencies found in the order";
        public const string EmptyCart = "There is no product in the cart";
    }
}
