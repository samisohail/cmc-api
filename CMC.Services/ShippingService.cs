using System;
using System.Collections.Generic;
using System.Text;
using CMC.Models;
using CMC.Services.Interface;

namespace CMC.Services
{
    public class ShippingService : IShippingService
    {
        private const string _baseCurrency = "AUD";

        private readonly ICurrencyService _conversionService;
        public ShippingService(ICurrencyService conversionService)
        {
            _conversionService = conversionService;
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
