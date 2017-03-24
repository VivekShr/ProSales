using AutoMapper;
using ProSales.Repository;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProSales.Service
{
    public class SalesTransactionService : ISalesTransactionService
    {
        private readonly ECommerceEntities context;
        private readonly IProductService productService;

        public SalesTransactionService(ECommerceEntities eContext, IProductService productService)
        {
            this.context = eContext;
            this.productService = productService;
        }

        public IQueryable<SalesTransaction> GetSalesTransaction()
        {
            var sales = (from s in context.SalesTransaction.AsQueryable()
                         from i in context.Invoice.Where(x => x.InvoiceId == s.InvoiceId).DefaultIfEmpty()
                         select s);
            return sales;
        }

        public IList<SalesTransactionViewModel> GetAllSalesOfCustomer(int customerId)
        {
            var sales = (from s in context.SalesTransaction.AsQueryable()
                         from i in context.Invoice.Where(x => x.InvoiceId == s.InvoiceId).DefaultIfEmpty()
                         where (customerId == 0 || s.CustomerId == customerId)
                         orderby s.SalesDate descending
                         select new SalesTransactionViewModel()
                         {
                             CustomerId = s.CustomerId,
                             CustomerName = s.Customer.Name,
                             InvoiceNumber = i.InvoiceNumber,
                             Price = s.Price,
                             ProductId = s.ProductId,
                             ProductName = s.Product.Name,
                             Quantity = s.Quantity,
                             SalesDate = s.SalesDate ?? DateTime.Now,
                             SalesTransactionId = s.SalesTransactionId
                         }).ToList();
            
            //var result = Mapper.Map<List<SalesTransaction>, List<SalesTransactionViewModel>>(sales);
           
            return sales;
        }
        public IList<SalesTransactionViewModel> GetTodaySalesOfCustomer(int customerId, DateTime date)
        {
            var salesTransaction = this.GetSalesTransaction().Where(x => (customerId == 0 || x.CustomerId == customerId) && x.SalesDate.Value.Date == date.Date).ToList();
            var result = Mapper.Map<List<SalesTransaction>, List<SalesTransactionViewModel>>(salesTransaction);

            return result;
        }

        public IList<SalesTransaction> GetUninvoicedSalesOfCustomer(int customerId, DateTime dateField)
        {
            var date = dateField.Date;
            var salesTransactions = this.GetSalesTransaction().Where(x => (customerId == 0 || x.CustomerId == customerId) && x.InvoiceId == null && System.Data.Objects.EntityFunctions.TruncateTime(x.SalesDate) == date).ToList();

            return salesTransactions;
        }

        public void AddSalesTransaction(SalesTransactionViewModel model)
        {
            if (model.ProductId == null || model.CustomerId == null)
	        {
		        throw new Exception("Invalid Sales Transaction");
	        }
            if (model != null || model.SalesTransactionId == 0)
            {
                SalesTransaction sales = Mapper.Map<SalesTransactionViewModel, SalesTransaction>(model);
                var product = productService.GetProductById(model.ProductId ?? 0);
                var productPrice = productService.ProductPrice(product);
                sales.Total = productPrice * model.Quantity ?? 1;
                sales.SalesDate = DateTime.Now;
                sales.Price = productPrice;
                context.SalesTransaction.Add(sales);
                context.SaveChanges();
            }
        }
        public void EditSalesTransaction(SalesTransactionViewModel model)
        {
            if (model.ProductId == null || model.CustomerId == null || model.SalesTransactionId == 0)
            {
                throw new Exception("Invalid Sales Transaction");
            }
            if (model != null || model.SalesTransactionId == 0)
            {
                SalesTransaction sales = Mapper.Map<SalesTransactionViewModel, SalesTransaction>(model);
                var product = productService.GetProductById(model.ProductId ?? 0);
                var productPrice = productService.ProductPrice(product);
                sales.Total = productPrice * model.Quantity ?? 1;
                sales.SalesDate = DateTime.Now;
                sales.Price = productPrice;
                context.SalesTransaction.Attach(sales);
                context.Entry<SalesTransaction>(sales).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public SalesTransactionViewModel GetById(int salesTransactionId)
        {
            var salesTransaction = this.context.SalesTransaction.Where(x => x.SalesTransactionId == salesTransactionId).SingleOrDefault();
            return Mapper.Map<SalesTransaction, SalesTransactionViewModel>(salesTransaction);
        }

        public decimal GetTotalAmountOfDay(int customerId, DateTime date)
        {
            var total = this.context.SalesTransaction.Where(x => (customerId == 0 || x.CustomerId == customerId && x.InvoiceId == null) && x.SalesDate.Value.Date == date.Date).Select(x => x.Total).Sum();
            return total ?? 0;
        }

        public decimal GetTotalAmountOfDay(List<SalesTransactionViewModel> salesTransactions)
        {
            var total = salesTransactions.Select(x => x.Total).Sum();
            return total ?? 0;
        }
    }
}