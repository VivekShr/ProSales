using Autofac;
using Autofac.Integration.Mvc;
using ProSales.Service;
using ProSales.Repository;
using ProSales.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProSales.IOC
{
    public static class IocConfigurator
    {
        public static void ConfigureDependencyInjection()
        {
            #region Create the builder
            var builder = new ContainerBuilder();
            #endregion

            #region Setup a common pattern
            // placed here before RegisterControllers as last one wins
            //builder.RegisterAssemblyTypes()
            //       .Where(t => t.Name.EndsWith("Repository"))
            //       .AsImplementedInterfaces()
            //       .InstancePerDependency();
            builder.RegisterAssemblyTypes()
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerRequest();
            #endregion

            #region Register all controllers for the assembly
            // Note that ASP.NET MVC requests controllers by their concrete types, 
            // so registering them As<IController>() is incorrect. 
            // Also, if you register controllers manually and choose to specify 
            // lifetimes, you must register them as InstancePerDependency() or 
            // InstancePerHttpRequest() - ASP.NET MVC will throw an exception if 
            // you try to reuse a controller instance for multiple requests. 
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();

            #endregion

            #region Register modules
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            #endregion

            builder.RegisterType<ECommerceEntities>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerRequest();
            builder.RegisterType<ECommerceEntities>().InstancePerRequest();
            builder.RegisterType<SalesTransactionService>().As<ISalesTransactionService>().InstancePerRequest();
            //builder.RegisterType<ECommerceEntities>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceService>().As<IInvoiceService>().InstancePerRequest();
            //builder.Register(c => new A(c.Resolve<B>()));
            //builder.Register(c =>
            //{
            //    var result = new InstaLink.ViewModelService.PasswordHasher();
            //    var dep = c.Resolve<IPasswordHasher>();
            //    result.SetTheDependency(dep);
            //    return result;
            //});


            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}