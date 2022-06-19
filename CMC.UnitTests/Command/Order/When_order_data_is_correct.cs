using System;
using System.Collections.Generic;
using System.Text;
using CMC.Commands.Order;
using CMC.Models;
using CMC.Models.Order;
using CMC.Services.Interface;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace CMC.UnitTests.Command.Order
{
    public class When_order_data_is_correct
    {
        [Fact]
        public void It_should_create_order_and_return_order_info()
        {
            // arrange
            var data = new CreateOrderRequest
            {
                Address = "68 Ferguson Avenue, Wiley Park, NSW",
                CustomerId = 1,
                CartItems = new List<CartItemDto>
                {
                    new CartItemDto {Currency = "AUD", Quantity = 1, ProductId = 1},
                    new CartItemDto {Currency = "AUD", Quantity = 2, ProductId = 6}
                }
            };
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(mock => mock.ValidateOrderItems(data.CartItems)).Returns(Result.OK(true));

            var commandHandler = new CreateOrderCommand.Handler(orderServiceMock.Object);

            // act
            var result = commandHandler.UnitTestHandle(new CreateOrderCommand {OrderCheckoutRequest = data});

            // assert
            Assert.NotNull(result);
        }
    }
}
