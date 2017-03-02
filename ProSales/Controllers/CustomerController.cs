using ProSales.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProSales.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        
        // GET: /Customer/
        public ActionResult Index()
        {
            var customers = customerService.GetCustomers();
            return View(customers);
        }
	}
}