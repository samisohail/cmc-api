using System;
using System.Collections.Generic;
using System.Linq;
using CMC.Domain.Cart;
using CMC.Models;
using CMC.Repositories.Interfaces;
using CMC.Services.Interface;
using Microsoft.Extensions.Configuration;

namespace CMC.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IEnumerable<Currency> _currencies;
        private readonly string _baseCurrency;
        public CurrencyService(ICurrencyRepository currencyRepo, IConfiguration configuration)
        {
            _baseCurrency = configuration.GetSection("BaseCurrency").Value;
            _currencies = currencyRepo.GetCurrencies().Value;
        }
        public Result<double> DoConversion(string fromCurrency, string toCurrency, double amount)
        {
            if (string.Equals(fromCurrency, toCurrency, StringComparison.OrdinalIgnoreCase))
                return Result.OK(amount);


            // base currency (aud) to some other currency conversion
            var fromCurrencyRatio = _currencies.SingleOrDefault(c => c.Name == fromCurrency)?.CurrencyToAudRatio ?? 0;
            var toCurrencyRatio = _currencies.SingleOrDefault(c => c.Name == toCurrency)?.CurrencyToAudRatio ?? 0;

            if (fromCurrencyRatio == 0 || toCurrencyRatio == 0)
                return Result.Fail<double>("Currency conversion ratio not defined");

            double ratio = default;
            if (toCurrency == _baseCurrency) // other currency to AUD
            {
                ratio = 1 / fromCurrencyRatio;
            }
            else // AUD to other currency
            {
                ratio = toCurrencyRatio;
            }

            var conversionAmount = amount * ratio;
            return Result.OK(Math.Round(conversionAmount, 2, MidpointRounding.AwayFromZero));
        }

        public string GetBaseCurrency()
        {
            return _baseCurrency;
        }

        public Result<bool> IsValidCurrency(string currency)
        {
            var currencyFound = _currencies.FirstOrDefault(c =>
                string.Equals(c.Name, currency, StringComparison.OrdinalIgnoreCase));

            if (currencyFound == null)
            {
                return Result.Fail<bool>(ErrorMessages.InvalidCurrency, true);
            }

            return Result.OK<bool>(true);
        }
    }
}
