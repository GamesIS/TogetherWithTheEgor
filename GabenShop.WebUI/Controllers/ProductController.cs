using GabenShop.Domain.Abstract;
using GabenShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GabenShop.WebUI.Models;

namespace GabenShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int pageSize = 4;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List(int page = 1)
        {
            //return View(repository.Products);
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                .OrderBy(Product => Product.ProductID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Products.Count()
                }
            };
            return View(model);
        }
    }
}