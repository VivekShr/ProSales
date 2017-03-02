using ProSales.Repository;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
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
            AutoMapper.Mapper.CreateMap<Customer, CustomerViewModel>();
            var viewModel = AutoMapper.Mapper.Map<List<Customer>, List<CustomerViewModel>>(Customers);
            
            return viewModel;
        }

        public IList<SelectModel> GetCustomerDdl()
        {
            return context.Customer.Select(x => new SelectModel()
            {
                Id = x.CustomerId.ToString(),
                Value = x.Name
            }).ToList();
        }
    }
}