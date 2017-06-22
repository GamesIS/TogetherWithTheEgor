using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GabenShop.WebUI.Models;
using System.Threading.Tasks;

namespace GabenShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private AccountModel accountModel = AccountModel.Instance;

        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (accountModel.IsUserExists(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("List", "Product");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");
                return View();
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("List", "Product");
        }

        [ChildActionOnly]
        public ActionResult UserBlock()
        {
            if (accountModel.IsInRole(User.Identity.Name, "Administrator") || accountModel.IsInRole(User.Identity.Name, "User"))
            {
                return PartialView("_LoggedUserPartial", accountModel);
            }
            else
            {
                return PartialView("_GuestUserPartial");
            }
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Login model, string returnUrl)
        {
            if (!accountModel.IsReadyToRegistration(model.UserName))
            {                
                accountModel.Registration(model.UserName, model.Password);
                FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                return RedirectToAction("List", "Product");                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Такой логин уже существует");
                return View();
            }
        }
    }
}