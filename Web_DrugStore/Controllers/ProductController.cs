using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;

namespace Web_DrugStore.Controllers
{
    public class ProductController : Controller
    {
        // Khởi tạo DbContext
        DS_DBContext db = new DS_DBContext();

        // Hiển thị danh sách tất cả sản phẩm
        public ActionResult AllProducts()
        {

            List<SanPham> sanphams = db.SanPhams.ToList();
            return View(sanphams);
        }

        // Hiển thị form tạo sản phẩm
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult DeleteAllProd()
        {
            
                var allSanPhams = db.SanPhams.ToList();
                db.SanPhams.RemoveRange(allSanPhams);
                db.SaveChanges();
            
            return RedirectToAction("AllProducts");
        }
        // Xử lý việc tạo sản phẩm mới
        [HttpPost]
        public ActionResult Create(SanPham sanPham, HttpPostedFileBase hinhAnh)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu có file hình ảnh được upload
                if (hinhAnh != null && hinhAnh.ContentLength > 0)
                {
                    try
                    {
                        // Lấy tên file
                        var fileName = Path.GetFileName(hinhAnh.FileName);
                        // Tạo đường dẫn đầy đủ tới thư mục lưu trữ hình ảnh
                        var path = Path.Combine(Server.MapPath("~/Content/img/KhoSanPham/"), fileName);

                        // Kiểm tra và tạo thư mục nếu chưa tồn tại
                        if (!Directory.Exists(Server.MapPath("~/Content/img/KhoSanPham/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/img/KhoSanPham/"));
                        }

                        // Lưu file hình ảnh vào thư mục
                        hinhAnh.SaveAs(path);

                        // Lưu tên file vào cơ sở dữ liệu
                        sanPham.HinhAnh = fileName;
                    }
                    catch (Exception ex)
                    {
                        // Thông báo lỗi nếu có vấn đề khi upload file
                        ModelState.AddModelError("", "Lỗi khi upload hình ảnh: " + ex.Message);
                        return View(sanPham);
                    }
                }

                // Thêm sản phẩm mới vào cơ sở dữ liệu
                db.SanPhams.Add(sanPham);
                db.SaveChanges();

                // Chuyển hướng về danh sách sản phẩm sau khi tạo
                return RedirectToAction("Create");
            }

            return View(sanPham);
        }
    }
}
