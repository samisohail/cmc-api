using System;
using System.Collections.Generic;
using System.Text;
using CMC.Services;
using CMC.Services.Interface;
using Moq;
using Xunit;

namespace CMC.UnitTests.Framework.Services
{
    public class When_order_greater_than_50
    {
        [Theory]
        [InlineData(50.01)]
        [InlineData(80)]
        [InlineData(100)]

        public void It_should_return_20(double orderTotal)
        {
            // arrange
            var currencyServiceMock = new Mock<ICurrencyService>();
            IShippingService shippingService = new ShippingService(currencyServiceMock.Object);

            // act
            var result = shippingService.GetShippingCost(orderTotal);

            // assert
            Assert.Equal(20, result);
        }

        [Theory]
        [InlineData(49.99)]
        public void It_should_fail_as_expected_cost_is_20_but_actual_is_10(double orderTotal)
        {
            // arrange
            var currencyServiceMock = new Mock<ICurrencyService>();
            IShippingService shippingService = new ShippingService(currencyServiceMock.Object);

            // act
            var result = shippingService.GetShippingCost(orderTotal);

            // assert
            Assert.NotEqual(20,  result);
        }
    }
}
