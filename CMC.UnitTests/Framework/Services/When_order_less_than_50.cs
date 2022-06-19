using System;
using System.Collections.Generic;
using System.Text;
using CMC.Services;
using CMC.Services.Interface;
using Moq;
using Xunit;

namespace CMC.UnitTests.Framework.Services
{
    public class When_order_less_than_50
    {
        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(40)]
        [InlineData(49.99)]
        [InlineData(50)]

        public void It_should_return_shipping_cost_of_10(double orderTotal)
        {
            // Arrange
            var currencyServiceMock = new Mock<ICurrencyService>();
            IShippingService shippingService = new ShippingService(currencyServiceMock.Object);

            // act
            var result = shippingService.GetShippingCost(orderTotal);

            // assert
            Assert.Equal(10, result);
        }
    }
}