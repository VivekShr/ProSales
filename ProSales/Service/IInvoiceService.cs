using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProSales.Service
{
    public interface IInvoiceService
    {
        IEnumerable<InvoiceViewModel> GetInvoices();
        IList<InvoiceViewModel> GetInvoicesOfCustomer(int customerId);
        InvoiceViewModel GetInvoiceById(int invoiceId);
        int CreateInvoiceForCustomer(int customerId, DateTime date);
        InvoiceViewModel GetDetail(int invoiceId);
    }
}