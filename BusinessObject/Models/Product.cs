using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessObject
{
    public class Product
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public decimal? UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public Category? Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}

