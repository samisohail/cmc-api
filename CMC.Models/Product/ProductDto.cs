using System;
using System.Collections.Generic;
using System.Text;

namespace CMC.Models.DTO
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Currency { get; set; }
    }
}
