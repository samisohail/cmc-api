using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CMC.Domain.Cart;
using CMC.Models;
using CMC.Repositories.DbEntities;
using CMC.Repositories.Interfaces;

namespace CMC.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Result<List<Product>> GetProducts()
        {
            var dbProducts = _products.Where(p => p.Active);
            var products = _mapper.Map<List<Product>>(dbProducts);
            return Result.OK(products);
        }

        public Result<List<Product>> ProductsById(int[] productId)
        {
            var dProducts = _products
                .Where(p => p.Active && productId.Contains(p.ProductId))
                .ToList();

            var products = _mapper.Map<List<Product>>(dProducts);
            return Result.OK(products);
        }

        private readonly List<DbProduct> _products = new List<DbProduct>()
        {
            new DbProduct{ ProductId = 1,Name = "IPhone", UnitPrice = 1000, Currency = "AUD", CreatedOn = DateTime.UtcNow, Active = true },
            new DbProduct{ ProductId = 2,Name = "Laptop", UnitPrice = 1000, Currency = "AUD", CreatedOn = DateTime.UtcNow, Active = true },
            new DbProduct{ ProductId = 3,Name = "Smart Watch", UnitPrice = 500, Currency = "AUD", CreatedOn = DateTime.UtcNow, Active = true },
            new DbProduct{ ProductId = 4, Name = "Laptop Bag", UnitPrice = 400, Currency = "AUD", CreatedOn = DateTime.UtcNow, Active = false },
            new DbProduct{ ProductId = 5, Name = "Pen", UnitPrice = 10, Currency = "AUD", CreatedOn = DateTime.UtcNow, Active = true },
            new DbProduct{ ProductId = 6,Name = "Mobile Cover", UnitPrice = 40, Currency = "AUD", CreatedOn = DateTime.UtcNow, Active = true }
        };
    }

}
