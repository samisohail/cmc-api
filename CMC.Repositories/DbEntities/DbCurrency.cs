using System;
using System.Collections.Generic;
using System.Text;

namespace CMC.Repositories.DbEntities
{
    public class DbCurrency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public double CurrencyToAudRatio { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
    }
}
