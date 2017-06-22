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
        public int pageSize = 6;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List(string category, int page = 1, int sort = 1)
        {
            ProductsListViewModel model;
            if(sort == 1)
            {
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderByDescending(Product => Product.Price)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(Product => Product.Category == category).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }
            else
            {
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(Product => Product.Price)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(Product => Product.Category == category).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }                   
            return View(model);
        }

        public FileContentResult GetImage(int productID)
        {
            Product product = repository.Products
                .FirstOrDefault(g => g.ProductID == productID);

            if (product != null)
            {
                return File(product.Image, "jpg");
            }
            else
            {
                return null;
            }
        }

        public ActionResult Discount()
        {
            return PartialView("Discount");
        }
    }
}