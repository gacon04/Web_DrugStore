using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
    }
}