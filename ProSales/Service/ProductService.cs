using ProSales.Service;
using ProSales.Repository;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            AutoMapper.Mapper.CreateMap<Product, ProductViewModel>();
            var viewModel = AutoMapper.Mapper.Map<List<Product>, List<ProductViewModel>>(products);
            
            return viewModel;
        }
    }
}