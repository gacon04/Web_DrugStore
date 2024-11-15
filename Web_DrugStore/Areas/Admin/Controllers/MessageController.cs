using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Models;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AuthorizationFilter]
    public class MessageController : Controller
    {
        // GET: Admin/Message
        public ActionResult Index()
        {
            DS_DBContext db = new DS_DBContext();
            List<TinNhanLienHe> tinnhans = db.TinNhanLienHes.OrderByDescending(tmp => tmp.ThoiGian).ToList();
            return View(tinnhans);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new DS_DBContext())
                {
                    TinNhanLienHe tinnhan = db.TinNhanLienHes.Find(id);
                    if (tinnhan != null)
                    {
                        db.TinNhanLienHes.Remove(tinnhan);
                        db.SaveChanges();
                        TempData["Message"] = "Xóa tin nhắn thành công!";
                    }
                    else
                    {
                        TempData["Message"] = "Xoá không thành công ";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra, thử lại sau nhé ^^";
            }

            return RedirectToAction("Index");
        }
    }
}