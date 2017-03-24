using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProSales.ViewModel
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            SalesTransactions = new List<SalesTransactionViewModel>();
        }
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceTotal { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public List<SalesTransactionViewModel> SalesTransactions { get; set; }
    }
}