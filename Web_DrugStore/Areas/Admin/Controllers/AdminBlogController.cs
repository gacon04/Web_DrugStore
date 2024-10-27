using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    public class AdminBlogController : Controller
    {
        // GET: Admin/Blog
        public ActionResult Index()
        {
            return View();
        }
    }
}