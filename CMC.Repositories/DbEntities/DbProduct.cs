using System;
using System.Collections.Generic;
using System.Text;

namespace CMC.Repositories.DbEntities
{
    public class DbProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedByUserId { get; set; }
        public bool Active { get; set; }
    }
}
