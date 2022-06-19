using System.Collections.Generic;
using CMC.Domain.Cart;
using CMC.Models;
using CMC.Models.Order;

namespace CMC.Services.Interface
{
    public interface IProductService
    {
        public bool IsValidProduct(int productId);
        public bool AreValidProducts(int[] productId);
        public Result<List<Product>> GetAll();
        Result<List<Product>> GetProductsPriceInGivenCurrency(string toCurrency);
        public Result<double> ProductsTotalInBaseCurrency(IEnumerable<CartItemDto> cartItems);
    }
}
