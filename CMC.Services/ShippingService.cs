using System;
using System.Collections.Generic;
using System.Text;
using CMC.Models;
using CMC.Services.Interface;
using Microsoft.Extensions.Configuration;

namespace CMC.Services
{
    public class ShippingService : IShippingService
    {
        private readonly string _baseCurrency;

        private readonly ICurrencyService _conversionService;
        public ShippingService(ICurrencyService conversionService, IConfiguration configuration)
        {
            _conversionService = conversionService;
            _baseCurrency = configuration.GetSection("BaseCurrency").Value;
        }
        public double GetShippingCost(double orderTotal) => orderTotal <= 50 ? 10 : 20;

        public Result<double> GetShippingCost(double orderTotal, string toCurrency)
        {
            var shippingCost = GetShippingCost(orderTotal);
            var conversionResult = _conversionService.DoConversion(_baseCurrency, toCurrency, shippingCost);
            return conversionResult;
        }
    }
}
