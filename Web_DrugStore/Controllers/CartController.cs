using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Models;

namespace Web_DrugStore.Controllers
{
   
    public class CartController : Controller
    {
        // GET: Cart
        DS_DBContext db = new DS_DBContext();
        public ActionResult GioHangMenuPar()
        {
           
            return PartialView();
        }
        public ActionResult MyCart()
        {
            return View();
        }



    }
}