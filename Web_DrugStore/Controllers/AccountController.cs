using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_DrugStore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Session["Tk"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult WishList()
        {
            if (Session["Tk"]==null)
            {
                return RedirectToAction("Login", "Account");
            }    
            return View();
        }
        public ActionResult Cart()
        {
            if (Session["Tk"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult Checkout()
        {
            if (Session["Tk"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["Tk"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}