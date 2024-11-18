using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data.Entity;
using System.IO;
using Web_DrugStore.Filters;
namespace Web_DrugStore.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AuthorizationFilter]
    public class AdminProductController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        public ActionResult Index()
        {

            List<SanPham> sanpham = db.SanPhams.ToList();
            return View(sanpham);
        }
        public ActionResult AddProd()
        {

            var danhMucCha = db.DanhMucs
                .Where(cate => cate.ParentId != null && cate.HoatDong)
                .ToList();

            ViewBag.DanhMucList = danhMucCha;
            return View();
        }
        // bật nhận dữ liệu html
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProd(SanPham model)
        {
            if (ModelState.IsValid)
            {
                DS_DBContext db = new DS_DBContext();
                model.LuotYeuThich = 0;
                model.LuotMua = 0;
                model.Hot = false;
                model.NgayTao = DateTime.Now;
                model.NgayCapNhat = DateTime.Now;
                db.SanPhams.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Reload danh sách danh mục khi ModelState không hợp lệ
            var danhMucCha = db.DanhMucs
                .Where(cate => cate.ParentId != null && cate.HoatDong)
                .ToList();

            ViewBag.DanhMucList = danhMucCha;
            return View(model);
        }
        public ActionResult EditProd(int id)
        {
            SanPham model = db.SanPhams.Where(prod => prod.SanPhamId == id).FirstOrDefault();
            var danhMucSanPham = db.DanhMucs
                .ToList();

            ViewBag.DanhMucList = danhMucSanPham;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProd(SanPham model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DS_DBContext())
                {
                    var temp = db.SanPhams.Where(sp => sp.SanPhamId == model.SanPhamId).FirstOrDefault();
                    if (temp != null)
                    {
                        // Cập nhật các thuộc tính của sản phẩm
                        temp.DanhMucId = model.DanhMucId;
                        temp.TenSanPham = model.TenSanPham;
                        temp.DonGia = model.DonGia;
                        temp.SoLuong = model.SoLuong;
                        temp.PhanLoai = model.PhanLoai;
                        temp.HoatDong = model.HoatDong;
                        temp.Thumbnail = model.Thumbnail;
                        temp.MoTa = model.MoTa;
                        temp.CongDung = model.CongDung;
                        temp.QuyCach = model.QuyCach;
                        temp.LuuY = model.LuuY;
                        temp.NhaSanXuat = model.NhaSanXuat;
                        
                        temp.Hot = model.Hot;

                        // Cập nhật Ngày cập nhật
                        temp.NgayCapNhat = DateTime.Now;

                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteProd(int id)
        {
            try
            {
                using (var db = new DS_DBContext())
                {
                    var sanpham = db.SanPhams.Find(id);
                    if (sanpham != null)
                    {
                        var danhsachhinh = db.HinhAnhSanPhams.Where(hinh => hinh.SanPhamId == id).ToList();
                        foreach (var hinh in danhsachhinh)
                        {
                            db.HinhAnhSanPhams.Remove(hinh);
                        }
                        db.SanPhams.Remove(sanpham);
                        
                        db.SaveChanges();
                        
                        TempData["Message"] = "Xóa sản phẩm thành công!";
                    }
                    else
                    {
                        TempData["Message"] = "Xoá sản phẩm không thành công, vui lòng kiểm tra lại";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra khi xóa sản phẩm, vui lòng thử lại";
            }

            return RedirectToAction("Index");
        }

        public class UploadOneFile
        {
            [Required(ErrorMessage = "Phải chọn file upload")]
            [DataType(DataType.Upload)]
            [Display(Name = "Chọn file upload")]
            [FileExtensions(Extensions = "png,jpg,jpeg,gif", ErrorMessage = "Chỉ chấp nhận các định dạng png, jpg, jpeg, gif")]
            public HttpPostedFileBase FileUpload { get; set; }
        }
        // GET: Upload Photo
        [HttpGet]
        public ActionResult UploadPhoto(int id)
        {
            var sanpham = db.SanPhams
                            .Where(p => p.SanPhamId == id)
                            .Include(img => img.HinhAnhSanPhams)
                            .FirstOrDefault();
            if (sanpham == null)
            {
                return HttpNotFound();
            }

            ViewData["sanpham"] = sanpham;
            return View(new UploadOneFile());
        }

        // POST: Upload Photo
        [HttpPost]
        public ActionResult UploadPhoto(int id, UploadOneFile f)
        {
            var product = db.SanPhams
                            .Where(sp => sp.SanPhamId == id)
                            .Include(p => p.HinhAnhSanPhams)
                            .FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound("Không có sản phẩm");
            }

            ViewData["sanpham"] = product;

            if (f?.FileUpload != null && f.FileUpload.ContentLength > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                               + Path.GetExtension(f.FileUpload.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/CKFinderPic/Images/SanPham"), fileName);

                // Lưu file lên server
                f.FileUpload.SaveAs(filePath);

                // Thêm đường dẫn vào cơ sở dữ liệu
                db.HinhAnhSanPhams.Add(new HinhAnhSanPham
                {
                    SanPhamId = product.SanPhamId,
                    DuongDan = "Content/CKFinderPic/Images/SanPham/" + fileName
                });

                db.SaveChanges();

            }
            else
            {
                ModelState.AddModelError("FileUpload", "Vui lòng chọn file.");
            }

            return View(f);
        }
        
        [HttpPost]
        public ActionResult ListPhotos(int id)
        {
            var product = db.SanPhams
                            .Where(sp => sp.SanPhamId == id)
                            .Include(p => p.HinhAnhSanPhams)
                            .FirstOrDefault();
            if (product == null)
            {
                return Json(
                    new
                    {
                        success = 0,
                        message = "Product Not Found"
                    }, JsonRequestBehavior.AllowGet);

            }
            var listPhotos = product.HinhAnhSanPhams.Select(photo => new // kiểu vô danh tự định nghĩa
            {
                id = photo.Id,
                path = photo.DuongDan
            });
            return Json(
                new
                {
                    success = 1,
                    photos = listPhotos

                }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult DeletePhoto(int id)
        {
            var photo = db.HinhAnhSanPhams.Where(p => p.Id == id).FirstOrDefault();
            if (photo != null)
            {
                db.HinhAnhSanPhams.Remove(photo);
                db.SaveChanges();
                var filenameRelative = "~/" + photo.DuongDan;
                var absolutePath = Server.MapPath(filenameRelative);
                System.IO.File.Delete(absolutePath);
            }
            return new HttpStatusCodeResult(200);
        }
        [HttpPost]
        public ActionResult UploadPhotoAPI(int id, UploadOneFile f)
        {
            var product = db.SanPhams
                            .Where(sp => sp.SanPhamId == id)
                            .Include(p => p.HinhAnhSanPhams)
                            .FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound("Không có sản phẩm");
            }


            if (f?.FileUpload != null && f.FileUpload.ContentLength > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                               + Path.GetExtension(f.FileUpload.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/CKFinderPic/Images/SanPham"), fileName);

                // Lưu file lên server
                f.FileUpload.SaveAs(filePath);

                // Thêm đường dẫn vào cơ sở dữ liệu
                db.HinhAnhSanPhams.Add(new HinhAnhSanPham
                {
                    SanPhamId = product.SanPhamId,
                    DuongDan = "Content/CKFinderPic/Images/SanPham/" + fileName
                });

                db.SaveChanges();

            }
            else
            {
                ModelState.AddModelError("FileUpload", "Vui lòng chọn file.");
            }

            return new HttpStatusCodeResult(200);
        }
    }
}