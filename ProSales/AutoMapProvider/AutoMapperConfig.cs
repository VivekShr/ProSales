
using AutoMapper;
using ProSales.Repository;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProSales.AutoMapProvider
{
    public class AutoMapperConfig
    {
        public static void Configure ()
	    {   
            Mapper.Initialize(cfg => {
                cfg.CreateMap<SalesTransaction, SalesTransactionViewModel>().AfterMap((x,y) => y.InvoiceNumber = x.Invoice == null ? string.Empty: x.Invoice.InvoiceNumber);
                cfg.CreateMap<SalesTransactionViewModel, SalesTransaction>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<CustomerViewModel, Customer>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<Invoice, InvoiceViewModel>();
            });

            //Mapper.CreateMap<Product, ProductViewModel>();
            //Mapper.CreateMap<SalesTransaction, SalesTransactionViewModel>();
            //Mapper.CreateMap<SalesTransactionViewModel, SalesTransaction>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //Mapper.CreateMap<Customer, CustomerViewModel>();
            //Mapper.CreateMap<CustomerViewModel, Customer>().IgnoreAllPropertiesWithAnInaccessibleSetter();
	    }
    }
}