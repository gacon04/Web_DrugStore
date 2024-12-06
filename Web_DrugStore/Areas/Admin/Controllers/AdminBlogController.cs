using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Web_DrugStore.Filters;
using Web_DrugStore.Models;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AuthorizationFilter]
    public class AdminBlogController : Controller
    {
        // PHẦN QUẢN LÝ DANH MỤC BÀI VIẾT
        public ActionResult BlogCateIndex() // trang chính xem danh sách danh mục bài viết
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
       
        [HttpPost]
        public ActionResult BlogCateDelete(int id)
        {
            try
            {
                using (var db = new DS_DBContext())
                {
                    var danhMucBlog = db.DanhMucBlogs.Find(id);
                    if (danhMucBlog != null)
                    {
                        db.DanhMucBlogs.Remove(danhMucBlog);
                        db.SaveChanges();
                        TempData["Message"] = "Xóa danh mục bài viết thành công!";
                    }
                    else
                    {
                        TempData["Message"] = "Xoá danh mục bài viết không thành công";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra khi xóa danh mục bài viết, vui lòng thử lại";
            }

            return RedirectToAction("BlogCateIndex"); // Sau khi xóa, quay lại trang danh sách
        }


        // PHẦN QUẢN LÝ BÀI VIẾT
        public ActionResult BlogIndex(int? page, string searchText)  // trang chính xem danh sách bài viết
        {
            DS_DBContext db = new DS_DBContext();
            var blogList = db.Blogs.AsQueryable();
            int size = 10;
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(searchText))
            {
                blogList = blogList.Where(sp => sp.TieuDe.Contains(searchText));
            }
            blogList = blogList.OrderBy(sp => sp.CreatedAt);
            var paginatedDH = blogList.ToPagedList(pageNumber, size);

            // Truyền `searchText` để giữ giá trị trong giao diện
            ViewBag.SearchText = searchText;

            return View(paginatedDH);
        }
        public ActionResult BlogAdd()
        {
            DS_DBContext db = new DS_DBContext();
            var danhMucBaiViet = db.DanhMucBlogs
                .Where(cate => cate.HoatDong == true)
                .ToList();

            ViewBag.DanhMucList = danhMucBaiViet;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogAdd(Blog model)
        {
            if (ModelState.IsValid)
            {
                DS_DBContext db = new DS_DBContext();
                model.IdTacGia = 1;
                model.CreatedAt = DateTime.Now;
                db.Blogs.Add(model);
                db.SaveChanges();

            }
            return RedirectToAction("BlogIndex");
        }
        public ActionResult BlogEdit(int id)
        {
            DS_DBContext db = new DS_DBContext();
            Blog model = db.Blogs.Where(blog => blog.Id == id).FirstOrDefault();
            var danhMucBaiViet = db.DanhMucBlogs
                .Where(cate => cate.HoatDong != false)
                .ToList();

            ViewBag.DanhMucList = danhMucBaiViet;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogEdit(Blog model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DS_DBContext())
                {
                    var temp = db.Blogs.Where(blog => blog.Id == model.Id).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.DanhMucBlogId = model.DanhMucBlogId;
                        temp.TieuDe = model.TieuDe;
                        temp.HienThi = model.HienThi;
                        temp.DuongDanHinhAnh = model.DuongDanHinhAnh;
                        temp.NoiDung = model.NoiDung;

                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("BlogIndex");
        }

        [HttpPost]
        public ActionResult BlogDelete(int id)
        {
            try
            {
                using (var db = new DS_DBContext())
                {
                    var blog = db.Blogs.Find(id);
                    if (blog != null)
                    {
                        db.Blogs.Remove(blog);
                        db.SaveChanges();
                        TempData["Message"] = "Xóa bài viết thành công!";
                    }
                    else
                    {
                        TempData["Message"] = "Xoá bài viết không thành công, vui lòng kiểm tra lại";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra khi xóa bài viết, vui lòng thử lại";
            }

            return RedirectToAction("BlogIndex"); // Sau khi xóa, quay lại trang danh sách
        }

    }
}