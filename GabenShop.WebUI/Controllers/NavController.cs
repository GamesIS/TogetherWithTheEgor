using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GabenShop.Domain.Abstract;


namespace GabenShop.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public PartialViewResult Menu(string category = null)
        {
            //ViewBag.SelectedCategory = category;// https://professorweb.ru/my/ASP_NET/gamestore/level2/2_4.php для выделения выбранной категории

            IEnumerable<string> categories = repository.Products
                .Select(game => game.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}