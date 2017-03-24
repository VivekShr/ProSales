using ProSales.Repository;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProSales.Service
{
    public interface IProductService
    {
        IList<ProductViewModel> GetProducts();
        Product GetProductById(int productId);
        IList<SelectModel> GetProductDll();
        decimal ProductPrice(Product product);
    }
}