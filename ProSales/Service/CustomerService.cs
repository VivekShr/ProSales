using ProSales.Repository;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;

namespace ProSales.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ECommerceEntities context;

        public CustomerService(ECommerceEntities eContext)
        {
            this.context = eContext;
        }
        public IList<CustomerViewModel> GetCustomers()
        {
            var Customers = context.Customer.ToList();
            var viewModel = AutoMapper.Mapper.Map<List<Customer>, List<CustomerViewModel>>(Customers);
            
            return viewModel;
        }

        public Customer GetCustomerById(int customerId)
        {
            return context.Customer.Where(x => x.CustomerId == customerId).SingleOrDefault();
        }

        public IList<SelectModel> GetCustomerDdl()
        {
            return context.Customer.Select(x => new SelectModel()
            {
                Id = SqlFunctions.StringConvert((double) x.CustomerId).Trim(),
                Value = x.Name
            }).ToList();
        }
    }
}