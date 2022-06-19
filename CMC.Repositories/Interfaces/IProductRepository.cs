using System.Collections.Generic;
using CMC.Domain.Cart;
using CMC.Models;

namespace CMC.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Result<List<Product>> GetProducts();
        public Result<List<Product>> ProductsById(int[] productId);
    }
}
