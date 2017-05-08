using GabenShop.Domain.Abstract;
using GabenShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GabenShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List()
        {
            return View(repository.Products);
        }
    }
}