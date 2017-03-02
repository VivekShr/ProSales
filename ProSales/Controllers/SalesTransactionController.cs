using ProSales.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProSales.Controllers
{
    public class SalesTransactionController : Controller
    {
        private readonly IProductService productService;
        public SalesTransactionController(IProductService productService)
        {
            this.productService = productService;
        }
        //
        // GET: /SalesTransaction/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProducts()
        {
            return View(productService.GetProducts());
        }
	}
}