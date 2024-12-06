using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Web_DrugStore.Identity;
using Web_DrugStore.Models;
using Web_DrugStore.ViewModel;
using System.Data.Entity;
using Web_DrugStore.Filters;
using Web_DrugStore.ViewModels;
namespace Web_DrugStore.Controllers
{
    
    public class AccountController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        [AuthenticationFilter]
        public ActionResult MyAccount()
        {
    
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM tmp)
        {
            if (ModelState.IsValid)
            {
                var appDBContext = new AppDBContext();
                var userStore = new AppUserStore(appDBContext);
                var userManager = new AppUserManager(userStore);
                // Truyền mật khẩu chưa mã hóa vào phương thức Create
                var findUserByEmail = userManager.FindByEmail(tmp.Email);
                var findUserBySDT = userManager.Users.FirstOrDefault(u => u.SDT == tmp.SDT);

                if (findUserByEmail != null)
                {
                    ModelState.AddModelError("Email", "Email đã được đăng ký.");
                }
                if (findUserBySDT != null)
                {
                    ModelState.AddModelError("SDT", "Số điện thoại đã được đăng ký.");
                }

                if (!ModelState.IsValid)
                {
                    return View(tmp);
                }

                var user = new AppUser()
                {
                    Email = tmp.Email,
                    HoTen = tmp.HoTen,
                    SDT = tmp.SDT,
                    DiaChi = tmp.DiaChi,
                    UserName = tmp.Email
                };
                IdentityResult identityResult = userManager.Create(user, tmp.MatKhau);
                if (identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    authenManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties(), userIdentity);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Lỗi", "Đăng ký không thành công.");
                }
            }
            else
            {
                ModelState.AddModelError("Lỗi", "Dữ liệu nhập vào không hợp lệ");
            }
            return View(tmp);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection f)
        {
            string tk = f["Email"];
            string mk = f["MatKhau"];
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(tk, emailPattern))
            {
                ViewData["Err1"] = "Vui lòng nhập tài khoản Email có định dạng hợp lệ";
                return View();
            }         
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            
            var user = userManager.FindByEmail(tk);
            if (user != null)
            {
                if (userManager.CheckPassword(user, mk))
                {
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties(), userIdentity);
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Err2"] = "Tài khoản/Mật khẩu không hợp lệ. Vui lòng kiểm tra lại";
                    return this.Login();
                }    

            }
                
            
            else
            {
                ViewData["Err2"] = "Thông tin tài khoản không tồn tại trên hệ thống";
                return this.Login();
            }
            return View();
        }

        [AuthenticationFilter]
        public ActionResult WishList()
        {
            return View();
        }

        
        [AuthenticationFilter]
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index", "Home");
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
        // PHẦN ĐỔI MẬT KHẨU

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
    }
}