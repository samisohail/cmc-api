using System.Collections.Generic;
using CMC.Domain.Cart;
using CMC.Models;
using CMC.Repositories.DbEntities;

namespace CMC.Repositories.Interfaces
{
    public interface ICurrencyRepository
    {
        public Result<List<Currency>> GetCurrencies();
    }
}
