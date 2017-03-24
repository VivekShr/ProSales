using AutoMapper;
using ProSales.Repository;
using ProSales.Utility;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProSales.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ECommerceEntities context;
        private readonly IProductService productService;
        private readonly ISalesTransactionService salesTransactionService;

        public InvoiceService(ECommerceEntities eContext, IProductService productService, ISalesTransactionService salesTransactionService)
        {
            this.context = eContext;
            this.productService = productService;
            this.salesTransactionService = salesTransactionService;
        }

        public IEnumerable<InvoiceViewModel> GetInvoices()
        {
            var invoices = (from s in this.context.SalesTransaction.AsQueryable()
                            join i in this.context.Invoice.AsQueryable() on s.InvoiceId equals i.InvoiceId
                            select new InvoiceViewModel()
                            {
                                CustomerId = s.CustomerId ?? 0,
                                CustomerName = s.Customer.Name,
                                InvoiceDate = i.InvoiceDate,
                                InvoiceId = i.InvoiceId,
                                InvoiceNumber = i.InvoiceNumber,
                                InvoiceTotal = i.InvoiceTotal ?? 0
                            }).Distinct();
            return invoices;
        }
        public IList<InvoiceViewModel> GetInvoicesOfCustomer(int customerId)
        {
            return this.GetInvoices().Where(x => x.CustomerId == customerId).ToList();
        }
        public InvoiceViewModel GetInvoiceById(int invoiceId)
        {
            throw new NotImplementedException();
        }
        public int CreateInvoiceForCustomer(int customerId, DateTime date)
        {
            var todaySales = salesTransactionService.GetUninvoicedSalesOfCustomer(customerId, date);
            decimal total = todaySales.Select(x => x.Total).Sum() ?? 0; //salesTransactionService.GetTotalAmountOfDay(customerId, date);
            if (total > 0)
            {
                //context.Database.Connection.Open();
                //using (var transaction = context.Database.Connection.BeginTransaction())
                //{
                    try
                    {
                        var invoice = new Invoice()
                        {
                            InvoiceDate = date,
                            InvoiceNumber = this.InvoiceNumberGenerator(),
                            InvoiceTotal = total
                        };
                        context.Invoice.Add(invoice);
                        foreach (var sale in todaySales)
                        {
                            sale.InvoiceId = invoice.InvoiceId;   
                        }
                        context.SaveChanges();
                        //transaction.Commit();
                        return invoice.InvoiceId;
                    }
                    catch (Exception ex)
                    {
                        //transaction.Rollback();
                        throw ex;
                    }
                //}
            }
            else
            {
                return 0;
            }
        }

        public string InvoiceNumberGenerator()
        {
            var curDate = DateTime.Now.ToString("yyyyMMdd");
            var randomNo = Helper.FourDigitRandomNumberGenerator();

            var invoiceNo = curDate + randomNo;

            return invoiceNo;
        }

        public InvoiceViewModel GetDetail(int invoiceId)
        {

            var q = context.Invoice.Where(x => x.InvoiceId == invoiceId).Select(x => new 
            {
                CustomerId = x.SalesTransaction.FirstOrDefault().CustomerId ?? 0,
                CustomerName = x.SalesTransaction.FirstOrDefault().Customer.Name,
                InvoiceDate = x.InvoiceDate,
                InvoiceId = x.InvoiceId,
                InvoiceNumber = x.InvoiceNumber,
                InvoiceTotal = x.InvoiceTotal ?? 0,
                SalesTransactions = x.SalesTransaction
                
            }).FirstOrDefault();

            var result = new InvoiceViewModel()
            {
                CustomerId = q.CustomerId,
                CustomerName = q.CustomerName,
                InvoiceDate = q.InvoiceDate,
                InvoiceId = q.InvoiceId,
                InvoiceNumber = q.InvoiceNumber,
                InvoiceTotal = q.InvoiceTotal,
                SalesTransactions = q.SalesTransactions.Select(x => new SalesTransactionViewModel()
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    InvoiceNumber = q.InvoiceNumber,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    SalesDate = x.SalesDate ?? DateTime.Now,
                    SalesTransactionId = x.SalesTransactionId
                }).ToList()
            };
            return result;
        }
    }
}