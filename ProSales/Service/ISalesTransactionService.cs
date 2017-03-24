using ProSales.Repository;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProSales.Service
{
    public interface ISalesTransactionService
    {
        IList<SalesTransactionViewModel> GetAllSalesOfCustomer(int customerId);
        IList<SalesTransactionViewModel> GetTodaySalesOfCustomer(int customerId, DateTime date);
        IList<SalesTransaction> GetUninvoicedSalesOfCustomer(int customerId, DateTime date);
        void AddSalesTransaction(SalesTransactionViewModel model);
        void EditSalesTransaction(SalesTransactionViewModel model);
        SalesTransactionViewModel GetById(int salesTransactionId);

        decimal GetTotalAmountOfDay(int customerId, DateTime date);
    }
}