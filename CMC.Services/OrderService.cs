using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMC.Models;
using CMC.Models.Order;
using CMC.Repositories.Interfaces;
using CMC.Services.Interface;

namespace CMC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepo;
        public OrderService(IProductService productService, IOrderRepository orderRepo)
        {
            _productService = productService;
            _orderRepo = orderRepo;
        }

        public Result<CreateOrderResponse> Create(CreateOrderRequest order)
        {
            var validateResult = ValidateOrderItems(order.CartItems);
            if (!validateResult.Success)
                return Result.Fail<CreateOrderResponse>(validateResult.Error);

            var createOrderResult = _orderRepo.Create(order);
            if (!createOrderResult.Success)
                return Result.Fail<CreateOrderResponse>(createOrderResult.Error);

            var response = new CreateOrderResponse
            {
                OrderId = createOrderResult.Value.OrderId,
                OrderRefNumber = createOrderResult.Value.OrderRefNumber
            };
            return Result.OK(response);
        }

        public Result<bool> ValidateOrderItems(IEnumerable<CartItemDto> cartItems)
        {
            var cartItemDtos = cartItems.ToList();

            if (!cartItemDtos.Any())
                return Result.Fail<bool>(ErrorMessages.EmptyCart);

            if (MultipleCurrenciesInCart(cartItemDtos))
                return Result.Fail<bool>(ErrorMessages.MultipleCurrenciesInCart);

            var productIds = cartItemDtos.Select(item => item.ProductId).ToArray();
            var validProducts = _productService.AreValidProducts(productIds);
            if (!validProducts)
                return Result.Fail<bool>(ErrorMessages.InvalidProduct, true);

            return Result.OK(true);
        }

        private bool MultipleCurrenciesInCart(IEnumerable<CartItemDto> items) =>
            items.Select(item => item.Currency).Distinct().Count() > 1;

    }
}
