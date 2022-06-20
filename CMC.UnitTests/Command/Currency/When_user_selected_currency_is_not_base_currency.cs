using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CMC.Models;
using CMC.Repositories.Interfaces;
using CMC.Services;
using CMC.Services.Interface;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace CMC.UnitTests.Command.Currency
{
    public class When_user_selected_currency_is_not_base_currency
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<ICurrencyRepository> _currencyRepoMock;
        
        public When_user_selected_currency_is_not_base_currency()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configuration/appsettings.json", false, false)
                .Build();

            _currencyRepoMock = new Mock<ICurrencyRepository>();
            _currencyRepoMock.Setup(mock => mock.GetCurrencies()).Returns(Result.OK(Currencies));
        }

        [Fact]
        public void It_should_return_true_for_aud()
        {
            // arrange   
            ICurrencyService currencyService = new CurrencyService(_currencyRepoMock.Object, _configuration);
            
            // act
            var result = currencyService.IsValidCurrency("AUD");

            // assert
            Assert.True(result.Value);
        }
        
        [Fact]
        public void It_should_return_correct_conversion_usd_to_aud()
        {
            // arrange
            ICurrencyService currencyService = new CurrencyService(_currencyRepoMock.Object, _configuration);

            // act
            var result = currencyService.DoConversion("USD", "AUD", 100);

            // assert
            Assert.Equal(142.86, result.Value);
        }
            
        [Fact]
        public void It_should_return_correct_conversion_aud_to_usd()
        {
            // arrange
            ICurrencyService currencyService = new CurrencyService(_currencyRepoMock.Object, _configuration);

            // act
            var result = currencyService.DoConversion("AUD", "USD", 100);

            // assert
            Assert.Equal(70, result.Value);
        }

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
            IShippingService shippingService = new ShippingService(currencyServiceMock.Object, _configuration);

            // act
            var result = shippingService.GetShippingCost(orderTotal);

            // assert
            Assert.Equal(10, result);
        }

        private List<Domain.Cart.Currency> Currencies =>
            new List<Domain.Cart.Currency>
            {
                Domain.Cart.Currency.Create(1, "AUD", "Australia", 1),
                Domain.Cart.Currency.Create(2, "USD", "USA", 0.70),
                Domain.Cart.Currency.Create(2, "GBP", "UK", 0.57),
                Domain.Cart.Currency.Create(2, "CAD", "UK", 0.91)
            };
    }
}
