using System;
using System.Collections.Generic;
using System.Text;
using CMC.Models;

namespace CMC.Services.Interface
{
    public interface ICurrencyService
    {
        public Result<double> DoConversion(string fromCurrency, string toCurrency, double amount);
        public Result<bool> IsValidCurrency(string currency);
        public string GetBaseCurrency();
    }
}
