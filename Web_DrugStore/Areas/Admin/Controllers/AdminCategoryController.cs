﻿using System;
using System.Collections.Generic;
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
            List<DanhMuc> danhmucs = db.DanhMucs.Include("DanhMucCha").ToList();
            return View(danhmucs);
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        public ActionResult CategoryDetails(int id)
        {
            return View();
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