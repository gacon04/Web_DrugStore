using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Identity;
using Web_DrugStore.Models;
using Web_DrugStore.ViewModels;

namespace Web_DrugStore.Controllers
{
    public class ActionAccountController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        [AuthenticationFilter]
        public ActionResult ChangePassword()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthenticationFilter]
        public ActionResult ChangePassConfirm(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var appDBContext = new AppDBContext();
                var userStore = new AppUserStore(appDBContext);
                var userManager = new AppUserManager(userStore);

                var user = userManager.FindById(userId);

                if (user != null)
                {
                    if (model.MatKhauHienTai == model.MatKhauMoi)
                    {
                        return Json(new { success = false, message = "Mật khẩu mới không được trùng với mật khẩu hiện tại." });
                    }

                    var result = userManager.ChangePassword(user.Id, model.MatKhauHienTai, model.MatKhauMoi);
                    if (result.Succeeded)
                    {
                        var authenManager = HttpContext.GetOwinContext().Authentication;
                        authenManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                        var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        authenManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties(), userIdentity);

                        return Json(new { success = true, message = "Đổi mật khẩu thành công." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Mật khẩu hiện tại không đúng." });
                    }
                }
                return Json(new { success = false, message = "Không tìm thấy người dùng." });
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }

        [AuthenticationFilter]
        public ActionResult GetListProd()
        {
            var userID = User.Identity.GetUserId();
            var listdh = db.DonHangs.Where(dh => dh.UserAspId == userID).OrderByDescending(dh => dh.MaDonHang);
            return PartialView(listdh);
        }
        [HttpGet]
        [AuthenticationFilter]
        public ActionResult OrderDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            var userID = User.Identity.GetUserId();

            var donHang = db.DonHangs
                           .Include("ChiTietDonHangs.SanPham")
                           .FirstOrDefault(dh => dh.MaDonHang == id && dh.UserAspId == userID);

            if (donHang == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DonHangDetailsViewModel
            {
                MaDonHang = donHang.MaDonHang,
                NgayDat = donHang.NgayDat,
                TrangThai = donHang.TrangThai,
                TongTienHang = donHang.TongTienHang,
                VAT = donHang.VAT,
                PhiVanChuyen = donHang.PhiVanChuyen,
                TongHoaDon = donHang.TongHoaDon,
                TenKhachHang = donHang.TenKhachHang,
                DiaChiGiaoHang = $"{donHang.SoNha}, {donHang.TenDuong}, {donHang.TenXa}, {donHang.TenHuyen}, {donHang.TenTinh}",
                SoDienThoai = donHang.SoDienThoai,
                DonHangChiTiet = donHang.ChiTietDonHangs.Select(ct => new DonHangChiTietViewModel
                {
                    MaSanPham = ct.SanPham.SanPhamId,
                    TenSanPham = ct.SanPham.TenSanPham,
                    SoLuong = ct.SoLuong,
                    DonGia = ct.DonGia
                }).ToList()
            };

            return PartialView("_OrderDetails", viewModel);
        }

        // thay doi trang thai don hang
        [HttpPost]
        [AuthenticationFilter]
        public JsonResult ChangeOrderStatus(string id, int newStatus)
        {
            var donHang = db.DonHangs.FirstOrDefault(dh => dh.MaDonHang == id);
            if (donHang == null)
            {
                return Json(new { success = false, message = "Đơn hàng không tồn tại!" });
            }

            // Kiểm tra trạng thái mới có hợp lệ không
            if (!Enum.IsDefined(typeof(TrangThaiDonHang), newStatus))
            {
                return Json(new { success = false, message = "Trạng thái đơn hàng không hợp lệ!" });
            }

            var trangThaiCu = donHang.TrangThai;
            var trangThaiMoi = (TrangThaiDonHang)newStatus;

            // Cập nhật trạng thái đơn hàng
            donHang.TrangThai = trangThaiMoi;
            db.SaveChanges();

            return Json(new { success = true, message = $"Cập nhật trạng thái đơn hàng thành công!", newStatus = trangThaiMoi.ToString() });
        }
    }
}