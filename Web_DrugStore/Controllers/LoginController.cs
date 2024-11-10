using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;

namespace Web_DrugStore.Controllers
{
    public class LoginController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection f, TaiKhoan tk)
        {
            string email = f["Email"];
            string matKhau = f["MatKhau"];

            // Tìm tài khoản trong cơ sở dữ liệu (giả sử bạn đã có một DbContext và bảng TaiKhoan)
            var user = db.TaiKhoans.FirstOrDefault(u => u.Email == email && u.MatKhau == matKhau);

            if (user != null)
            {
                Session["Tk"] = user;
 
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                ViewBag.ErrorMessage = "Email hoặc mật khẩu không đúng!";
                return View();
            }
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection f, TaiKhoan tk)
        {
            // Lấy thông tin từ form
            tk.HoTen = f["HoTen"];
            tk.Email = f["Email"];
            tk.SDT = f["SDT"];
            tk.MatKhau = f["MatKhau"];
            string passNhapLai = f["Confirmpassword"];
            tk.DiaChi = f["DiaChi"];
            tk.VaiTro = "Customer"; 

            if (tk.MatKhau != passNhapLai)
            {
                ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không khớp.");
                return View(tk);
            }
            if (Regex.IsMatch(tk.SDT, @"[a-zA-Z]"))
            {
                ModelState.AddModelError("", "Số điện thoại không hợp lệ. Vui lòng chỉ nhập số.");
                return View(tk);
            }

            // Kiểm tra email đã tồn tại chưa
            if (db.TaiKhoans.Any(t => t.Email == tk.Email))
            {
                ModelState.AddModelError("", "Email đã tồn tại.");
                return View(tk);
            }

            // Kiểm tra số điện thoại đã tồn tại chưa
            if (db.TaiKhoans.Any(t => t.SDT == tk.SDT))
            {
                ModelState.AddModelError("", "Số điện thoại đã tồn tại.");
                return View(tk);
            }

            Session["TK"] = tk;
            db.TaiKhoans.Add(tk);
            db.SaveChanges();

            // Thông báo đăng ký thành công
            ViewBag.Message = "Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.";

            return View();
        }
    }
}