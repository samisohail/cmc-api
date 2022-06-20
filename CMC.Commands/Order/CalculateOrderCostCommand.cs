using System;
using System.Collections.Generic;
using System.Linq;
using CMC.Services.Interface;
using CMC.Models;
using CMC.Models.Order;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CMC.Commands.Order
{
    public sealed class CalculateOrderCostCommand : IRequest<Result<OrderCostResponse>>
    {
        public IEnumerable<CartItemDto> CartItems { get; set; }

        public sealed class Handler : CommandBaseController<CalculateOrderCostCommand, Result<OrderCostResponse>>
        {
            private readonly IProductService _productService;
            private readonly ICurrencyService _currencyService;
            private readonly IShippingService _shippingService;
            private readonly IOrderService _orderService;
            
            public Handler(
                IProductService productService,
                ICurrencyService currencyService,
                IShippingService shippingService,
                IOrderService orderService)
            {
                _productService = productService;
                _currencyService = currencyService;
                _shippingService = shippingService;
                _orderService = orderService;
            }

            protected override Result<OrderCostResponse> Handle(CalculateOrderCostCommand request)
            {
                try
                {
                    var validCartResult = _orderService.ValidateOrderItems(request.CartItems);
                    if (!validCartResult.Success)
                        return Result.Fail<OrderCostResponse>(validCartResult.Error, validCartResult.NotFoundToModify);

                    var baseCurrency = _currencyService.GetBaseCurrency();
                    var cartCurrency = request.CartItems.First().Currency;
                    // var productIds = request.CartItems.Select(p => p.ProductId).ToArray();

                    var productsCostResult = _productService.ProductsTotalInBaseCurrency(request.CartItems);
                    if (!productsCostResult.Success)
                        return Result.Fail<OrderCostResponse>(productsCostResult.Error);


                    var shippingCost = _shippingService.GetShippingCost(productsCostResult.Value);

                    var productsCost = productsCostResult.Value;


                    if (cartCurrency != baseCurrency)
                    {
                        // products cost conversion
                        var conversionResult = _currencyService.DoConversion(baseCurrency, cartCurrency, productsCost);
                        if (!conversionResult.Success)
                            return Result.Fail<OrderCostResponse>(conversionResult.Error, conversionResult.NotFoundToModify);
                        
                        productsCost = conversionResult.Value;

                        // shipping cost conversion
                        conversionResult = _currencyService.DoConversion(baseCurrency, cartCurrency, shippingCost);
                        if (!conversionResult.Success)
                            return Result.Fail<OrderCostResponse>(conversionResult.Error, conversionResult.NotFoundToModify);
                        
                        shippingCost = conversionResult.Value;
                    }

                    var result = new OrderCostResponse
                    {
                        Currency = cartCurrency,
                        ProductsTotal = productsCost,
                        Shipping = shippingCost
                    };

                    return Result.OK(result);
                }
                catch (Exception e)
                {
                    // exception needs to be logged

                    return Result.Fail<OrderCostResponse>("Oops! an unhandled exception occurred");
                }
            }
        }
    }
}
