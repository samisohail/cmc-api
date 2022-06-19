using System.Collections.Generic;
using System.Linq;
using CMC.Domain.Cart;
using CMC.Models;
using CMC.Models.Order;
using CMC.Repositories.Interfaces;
using CMC.Services.Interface;
using Microsoft.Extensions.Configuration;

namespace CMC.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly ICurrencyService _currencyService;
        public ProductService(IProductRepository productRepo, ICurrencyService currencyService)
        {
            _productRepo = productRepo;
            _currencyService = currencyService;
        }
        public bool IsValidProduct(int productId)
        {
            var products = _productRepo.GetProducts().Value;
            var product = products.FirstOrDefault(p => p.ProductId == productId);
            return product != null;
        }

        public bool AreValidProducts(int[] productId)
        {
            var products = _productRepo.GetProducts().Value;
            return !productId.Except(products.Select(p => p.ProductId)).Any();
        }

        public Result<double> ProductsTotalInBaseCurrency(IEnumerable<CartItemDto> cartItems)
        {
            // all the prices in DB are in base currency i.e. AUD
            var productIds = cartItems.Select(i => i.ProductId).ToArray();
            var productsResult = _productRepo.ProductsById(productIds);

            if (productsResult.Value.Count() != cartItems.Count())
                return Result.Fail<double>(ErrorMessages.InvalidProduct, notFound: true);

            double total = 0;
            foreach (var item in cartItems)
            {
                var unitPrice = productsResult.Value.First(p => p.ProductId == item.ProductId).UnitPrice;
                total += unitPrice * item.Quantity;
            }

            return Result.OK(total);
        }

        

        public Result<List<Product>> GetAll()
        {
            return _productRepo.GetProducts();
        }

        public Result<List<Product>> GetProductsPriceInGivenCurrency(string toCurrency)
        {
            var productsResult = _productRepo.GetProducts();
            if (!productsResult.Success)
                return Result.Fail<List<Product>>(productsResult.Error);

            var products = productsResult.Value;
            foreach (var product in products)
            {
                var conversionResult = _currencyService.DoConversion("AUD", toCurrency, product.UnitPrice);
                if (!conversionResult.Success)
                    return Result.Fail<List<Product>>(conversionResult.Error);
                else
                {
                    product.SetPriceWithCurrency(conversionResult.Value, toCurrency);
                }
            }

            return Result.OK(products);
        }
    }
}
