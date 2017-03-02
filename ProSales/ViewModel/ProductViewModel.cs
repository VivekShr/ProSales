using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProSales.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string ProductImage { get; set; }
    }
}