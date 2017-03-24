using ProSales.Service;
using ProSales.Repository;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.SqlClient;

namespace ProSales.Service
{
    public class ProductService : IProductService
    {
        private readonly ECommerceEntities context;

        public ProductService(ECommerceEntities eContext)
        {
            this.context = eContext;
        }
        public IList<ProductViewModel> GetProducts()
        {
            var products = context.Product.ToList();
            
            var viewModel = AutoMapper.Mapper.Map<List<Product>, List<ProductViewModel>>(products);
            
            return viewModel;
        }

        public Product GetProductById(int productId)
        {
            return context.Product.Where(x => x.ProductId == productId).SingleOrDefault();
        }

        public decimal ProductPrice(Product product)
        {
            return product.SellingPrice ?? 0;
        }

        public IList<SelectModel> GetProductDll()
        {
            return context.Product.Select(x => new SelectModel()
            {
                Id = SqlFunctions.StringConvert((double)x.ProductId).Trim(),
                Value = x.Name + " (" + SqlFunctions.StringConvert((double)x.SellingPrice).Trim() + ")"
            }).ToList();
        }
    }
}