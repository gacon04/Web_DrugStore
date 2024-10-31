using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddProd()
        {
            DS_DBContext db = new DS_DBContext();
            var danhMucCha = db.DanhMucs
                .Where(cate => cate.ParentId != null)
                .ToList();

            ViewBag.DanhMucList = danhMucCha;
            return View();
        }
        // bật nhận dữ liệu html

    }
}