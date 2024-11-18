using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;

namespace Web_DrugStore.Controllers
{
    public class PaymentController : Controller
    {

        // GET: Payment
        [AuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}