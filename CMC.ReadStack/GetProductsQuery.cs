using System.Collections.Generic;
using AutoMapper;
using CMC.Models;
using CMC.Models.DTO;
using CMC.Services.Interface;
using MediatR;

namespace CMC.ReadStack
{
    public sealed class GetProductsQuery : IRequest<Result<List<ProductDto>>>
    {
        public string Currency { get; set; }
        public sealed class Handler : ReadStackBaseHandler<GetProductsQuery, Result<List<ProductDto>>>
        {
            private readonly IProductService _productService;

            public Handler(IMapper mapper, IProductService productService) : base(mapper)
            {
                _productService = productService;
            }
            protected override Result<List<ProductDto>> Handle(GetProductsQuery request)
            {
                var dbProducts = _productService.GetProductsPriceInGivenCurrency(request.Currency);
                if (!dbProducts.Success)
                    return Result.Fail<List<ProductDto>>(ErrorMessages.NoProductFound);

                var productsDto =  _mapper.Map<List<ProductDto>>(dbProducts.Value);
                return Result.OK(productsDto);
            }
        }
    }
}
