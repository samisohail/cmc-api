using System.Collections.Generic;
using AutoMapper;
using CMC.Models;
using CMC.Repositories.Interfaces;
using CMC.Services.Interface;
using MediatR;

namespace CMC.ReadStack
{
    public sealed class GetProductsQuery : IRequest<Result<List<ProductDto>>>
    {
        public sealed class Handler : ReadStackBaseHandler<GetProductsQuery, Result<List<ProductDto>>>
        {
            private readonly IProductService _productService;

            public Handler(IMapper mapper, IProductService productService) : base(mapper)
            {
                _productService = productService;
            }
            protected override Result<List<ProductDto>> Handle(GetProductsQuery request)
            {
                var productsResult = _productService.GetAll();
                if (!productsResult.Success)
                    return Result.Fail<List<ProductDto>>(productsResult.Error);

                var productsDto =  _mapper.Map<List<ProductDto>>(productsResult.Value);
                return Result.OK(productsDto);
            }
        }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Currency { get; set; }
    }
}
