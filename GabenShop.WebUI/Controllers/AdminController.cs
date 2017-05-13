using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GabenShop.Domain.Abstract;
using GabenShop.Domain.Entities;

namespace GabenShop.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productID)
        {
            Product product = repository.Products
                .FirstOrDefault(g => g.ProductID == productID);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)//Можно добавить проверку на стороне клиента
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productID)
        {
            Product deletedProduct = repository.DeleteProduct(productID);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}