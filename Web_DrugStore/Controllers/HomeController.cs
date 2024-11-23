using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Web_DrugStore.Models;
namespace Web_DrugStore.Controllers
{
    public class HomeController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewestProduct()
        {
            List<SanPham> sanphams = db.SanPhams.Where(prod => prod.HoatDong == true).OrderByDescending(prod => prod.NgayTao).Take(6).ToList();
            return PartialView(sanphams);
        }
        public ActionResult BestSellingProduct()
        {
            List<SanPham> sanphams = db.SanPhams.Where(prod => prod.HoatDong == true).OrderByDescending(prod => prod.LuotMua).Take(6).ToList();
            return PartialView(sanphams);
        }

         

    }
}