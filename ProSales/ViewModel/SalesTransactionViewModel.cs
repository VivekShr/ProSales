using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProSales.ViewModel
{
    public class SalesTransactionViewModel
    {
        //ProSales.Repository.SalesTransaction
        public int SalesTransactionId { get; set; }
        [Required]
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        [Required]
        public int? Quantity { get; set; }
        public decimal? Total { get {return this.Price * this.Quantity ?? 0;}}
        [Required]
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime SalesDate { get; set; }
        public string InvoiceNumber { get; set; }
    }
}