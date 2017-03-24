using ProSales.Service;
using ProSales.Utility;
using ProSales.ViewModel;
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
        private readonly ICustomerService customerService;
        private readonly ISalesTransactionService salesTransactionService;

        public SalesTransactionController(IProductService productService, ISalesTransactionService salesTransactionService, ICustomerService customerService)
        {
            this.productService = productService;
            this.salesTransactionService = salesTransactionService;
            this.customerService = customerService;
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

        public ActionResult Sales(int customerId = 0)
        {
            ViewBag.CustomerList = SelectModelToSelectListItemConverter.Convert(customerService.GetCustomerDdl());
            return View (salesTransactionService.GetAllSalesOfCustomer(customerId));
        }

        public ActionResult PartialSalesGrid(int customerId = 0)
        {
            return PartialView("_PartialSalesTransaction", salesTransactionService.GetAllSalesOfCustomer(customerId));
        }

        public ActionResult Add()
        {
            ViewBag.CustomerList = SelectModelToSelectListItemConverter.Convert(customerService.GetCustomerDdl());
            ViewBag.ProductList = SelectModelToSelectListItemConverter.Convert(productService.GetProductDll());
            return View("Add");
        }

        [HttpPost]
        public JsonResult Add(SalesTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json("0");
            }

            salesTransactionService.AddSalesTransaction(model);
            return Json("1");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Sales");
            }
            var salesTransaction = salesTransactionService.GetById(id);
            ViewBag.CustomerList = SelectModelToSelectListItemConverter.Convert(customerService.GetCustomerDdl(), id.ToString());
            ViewBag.ProductList = SelectModelToSelectListItemConverter.Convert(productService.GetProductDll(), id.ToString());
            
            return View("Edit", salesTransaction);
        }

        [HttpPost]
        public JsonResult Edit(SalesTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json("Invalid data.");
            }

            salesTransactionService.EditSalesTransaction(model);
            return Json("Data updated successfully.");
        }
	}
}