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
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService invoiceService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        private readonly ISalesTransactionService salesTransactionService;

        public InvoiceController(IInvoiceService invoiceService, IProductService productService, ISalesTransactionService salesTransactionService, ICustomerService customerService)
        {
            this.invoiceService = invoiceService;
            this.productService = productService;
            this.salesTransactionService = salesTransactionService;
            this.customerService = customerService;
        }
        
        // GET: /Invoice List of Customer/
        public ActionResult Index(int customerId = 0)
        {
            ViewBag.CustomerList = SelectModelToSelectListItemConverter.Convert(customerService.GetCustomerDdl());
            return View(invoiceService.GetInvoices().ToList());
            //return View();
        }

        public ActionResult PartialInvoiceGrid(int customerId = 0)
        {
            return PartialView("_PartialInvoiceGrid", invoiceService.GetInvoicesOfCustomer(customerId));
        }

        [HttpPost]
        public JsonResult Generate(int customerId, DateTime? salesDate)
        {
            try
            {
                int invoiceId = invoiceService.CreateInvoiceForCustomer(customerId, salesDate ?? DateTime.Now);

                if (invoiceId != 0)
                {
                    return Json(invoiceId);
                }
                else
                {
                    Response.StatusCode = 404;
                    return Json("No sales transaction left for this date. Please try selecting any other date that has sales transaction.");
                }

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json("Internal server error. Please contact Administrator for further support");
            }
        }

        public ActionResult Detail(int id)
        {
            InvoiceViewModel detail = invoiceService.GetDetail(id);
            return View(detail);
        }
	}
}