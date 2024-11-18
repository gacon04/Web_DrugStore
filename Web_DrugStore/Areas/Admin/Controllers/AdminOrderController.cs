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
    public class AdminOrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index()
        {
            return View();
        }
    }
}