using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    public class AdminBlogController : Controller
    {
        // GET: Admin/Blog
        public ActionResult BlogCateIndex()
        {
            DS_DBContext db = new DS_DBContext();
            List<DanhMucBlog> danhmucblogs = db.DanhMucBlogs.ToList();
            return View(danhmucblogs);
        }
        public ActionResult BlogCateAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BlogCateAdd(DanhMucBlog model)
        {
            if (ModelState.IsValid)
            {
                DS_DBContext db = new DS_DBContext();
                db.DanhMucBlogs.Add(model);
                db.SaveChanges();

            }
            return RedirectToAction("BlogCateIndex");
        }
        public ActionResult BlogCateEdit(int id)
        {
            DS_DBContext db = new DS_DBContext();
            DanhMucBlog model = db.DanhMucBlogs.Where(cate => cate.Id == id).FirstOrDefault();

            return View(model);
        }
        [HttpPost]
        public ActionResult BlogCateEdit(DanhMucBlog model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DS_DBContext())
                {
                    var temp = db.DanhMucBlogs.Where(cate => cate.Id == model.Id).FirstOrDefault();
                    if (temp != null)
                    {
                        // Gán các thuộc tính từ cateNew sang temp
                        temp.TenDanhMuc = model.TenDanhMuc;
                        temp.MoTa = model.MoTa;
                        temp.HoatDong = model.HoatDong;

                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("BlogCateIndex");
        }
        public ActionResult BlogCateDelete()
        {
            return View();
        }

    }
}