using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AuthorizationFilter]
    public class AdminHomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}