using System.Collections.Generic;
using AutoMapper;
using CMC.Models;
using CMC.Models.DTO;
using CMC.Services.Interface;
using MediatR;
using Serilog;

namespace CMC.ReadStack
{
    public sealed class GetProductsQuery : IRequest<Result<List<ProductDto>>>
    {
        public string Currency { get; set; }
        public sealed class Handler : ReadStackBaseHandler<GetProductsQuery, Result<List<ProductDto>>>
        {
            private readonly IProductService _productService;
            private readonly ILogger _logger;

            public Handler(IMapper mapper, IProductService productService, ILogger logger) : base(mapper)
            {
                _productService = productService;
                _logger = logger;
            }
            protected override Result<List<ProductDto>> Handle(GetProductsQuery request)
            {
                var dbProducts = _productService.GetProductsPriceInGivenCurrency(request.Currency);
                if (!dbProducts.Success)
                    return Result.Fail<List<ProductDto>>(ErrorMessages.NoProductFound);

                var productsDto =  _mapper.Map<List<ProductDto>>(dbProducts.Value);
                _logger.Information("Response {@request} - {@response} {}", request, productsDto);
                return Result.OK(productsDto);
            }
        }
    }
}
