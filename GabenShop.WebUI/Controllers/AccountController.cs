using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GabenShop.WebUI.Models;

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
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("_LoggedUserPartial");
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
    }
}