using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CMC.Domain.Cart;
using CMC.Models;
using CMC.Repositories.DbEntities;
using CMC.Repositories.Interfaces;

namespace CMC.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IMapper _mapper;
        public CurrencyRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Result<List<Currency>> GetCurrencies()
        {
            var currencies = _mapper.Map<List<Currency>>(Currencies);
            return Result.OK(currencies);
        }

        private readonly IEnumerable<DbCurrency> Currencies = new List<DbCurrency>()
        {
            new DbCurrency { CurrencyId = 1, Name = "AUD", Country = "AUD", CurrencyToAudRatio = 1, CreatedOn = DateTime.UtcNow, Active = true },
            new DbCurrency { CurrencyId = 2, Name = "USD", Country = "USA", CurrencyToAudRatio = 0.70, CreatedOn = DateTime.UtcNow, Active = true },
            new DbCurrency { CurrencyId = 3, Name = "GBP", Country = "UK", CurrencyToAudRatio = 0.57, CreatedOn = DateTime.UtcNow, Active = true },
            new DbCurrency { CurrencyId = 4, Name = "CAD", Country = "Canada", CurrencyToAudRatio = 0.91, CreatedOn = DateTime.UtcNow, Active = true }
        };
    }
}
