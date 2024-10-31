using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Web_DrugStore.Models;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    public class AdminCategoryController : Controller
    {
        
        // GET: Admin/Category
        public ActionResult Index()
        {
            DS_DBContext db = new DS_DBContext();
            List<DanhMuc> danhmucs = db.DanhMucs.OrderBy(dm => dm.ParentId == null ? 0 : 1).Include("DanhMucCha").ToList();
            return View(danhmucs);
        }
        public ActionResult AddCategory()
        {
            DS_DBContext db = new DS_DBContext();
            var danhMucCha = db.DanhMucs
                .Where(cate => cate.ParentId == null) 
                .ToList();

            ViewBag.DanhMucList = danhMucCha;
            return View(new DanhMuc());
        }



        [HttpPost]
        public ActionResult AddCategory(DanhMuc model)
        {
            if (ModelState.IsValid)
            {
                DS_DBContext db = new DS_DBContext();
                db.DanhMucs.Add(model);
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            DS_DBContext db = new DS_DBContext();
            DanhMuc temp = db.DanhMucs.Where( cate => cate.Id == id ).FirstOrDefault() ;
            var danhMucCha = db.DanhMucs
                .Where(cate => cate.ParentId == null)
                .ToList();

            ViewBag.DanhMucList = danhMucCha;

            return View(temp);
        }
        [HttpPost]
        public ActionResult Edit(DanhMuc cateNew)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DS_DBContext())
                {
                    var temp = db.DanhMucs.FirstOrDefault(cate => cate.Id == cateNew.Id);
                    if (temp != null)
                    {
                        // Gán các thuộc tính từ cateNew sang temp
                        temp.TenDanhMuc = cateNew.TenDanhMuc;
                        temp.ParentId = cateNew.ParentId;
                        temp.MoTa = cateNew.MoTa;
                        temp.HoatDong = cateNew.HoatDong;

                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new DS_DBContext())
                {
                    var danhMuc = db.DanhMucs.Find(id);
                    if (danhMuc != null)
                    {
                        db.DanhMucs.Remove(danhMuc);
                        db.SaveChanges();
                        TempData["Message"] = "Xóa danh mục thành công!";
                    }
                    else
                    {
                        TempData["Message"] = "Danh mục không tồn tại!";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra khi xóa danh mục. Đảm bảo không xoá danh mục cha";
            }

            return RedirectToAction("Index"); // Sau khi xóa, quay lại trang danh sách
        }


    }
}