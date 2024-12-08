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
using System.Net;
using Web_DrugStore.FuncService;
namespace Web_DrugStore.Controllers
{
    
    public class AccountController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        [Route("TaiKhoanCuaToi")]
        [AuthenticationFilter]
        public ActionResult MyAccount()
        {
    
            return View();
        }
        [Route("QuenMatKhau")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [Route("QuenMatKhau")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPassMailVM model)
        {
            if (ModelState.IsValid)
            {
                var appDBContext = new AppDBContext();
                var userStore = new AppUserStore(appDBContext);
                var userManager = new AppUserManager(userStore);

                var user = userManager.FindByEmail(model.Email);
                if (user != null)
                {
                    // Tạo token đặt lại mật khẩu
                    var token = userManager.GeneratePasswordResetToken(user.Id);

                    // Tạo liên kết đặt lại mật khẩu
                    var resetLink = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = token }, protocol: Request.Url.Scheme);
                    string subject = "Xác nhận đặt lại mật khẩu tại PharmaVillage";

                    string body = $@"
                    <!DOCTYPE html>
                    <html lang='vi'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                line-height: 1.6;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                                color: #333;
                            }}
                            .email-container {{
                                max-width: 600px;
                                margin: 40px auto;
                                padding: 40px;
                                background-color: #ffffff;
                                border-radius: 8px;
                                box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
                            }}
                            .email-header {{
                                text-align: center;
                                margin-bottom: 30px;
                            }}
                            .email-header h1 {{
                                font-size: 36px;
                                font-weight: 700;
                            }}
                            .email-header h1 span {{
                                color: #32cd32; /* Màu xanh lá chuối cho Pharma */
                            }}
                            .email-header h1 span:last-child {{
                                color: #000000; /* Màu đen cho Village */
                            }}
                            .email-content p {{
                                font-size: 16px;
                                margin: 15px 0;
                                color: #555;
                            }}
                            .email-link {{
                                display: inline-block;
                                margin: 20px auto;
                                padding: 12px 30px;
                                font-size: 18px;
                                color: #ffffff;
                                background-color: #007bff;
                                text-decoration: none;
                                border-radius: 5px;
                                text-align: center;
                                width: auto;
                                min-width: 200px;
                                text-transform: uppercase;
                                font-weight: bold;
                                transition: background-color 0.3s;
                            }}
                            .email-link:hover {{
                                background-color: #0056b3;
                            }}
                            .email-footer {{
                                text-align: center;
                                font-size: 14px;
                                color: #777;
                                margin-top: 30px;
                            }}
                            .email-footer p {{
                                margin: 5px 0;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='email-header'>
                                <h1><span>Pharma</span><span>Village</span></h1>
                            </div>
                            <div class='email-content'>
                                <p>Chào bạn,</p>
                                <p>Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn tại PharmaVillage. Để đặt lại mật khẩu, vui lòng nhấp vào liên kết bên dưới:</p>
                                <a href='{resetLink}' class='email-link'>Đặt lại mật khẩu</a>
                                <p>Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này. Liên kết trên sẽ hết hạn sau 1 giờ.</p>
                                <p>Cảm ơn bạn đã tin tưởng sử dụng dịch vụ của chúng tôi!</p>
                            </div>
                            <div class='email-footer'>
                                <p>Trân trọng,<br>Đội ngũ hỗ trợ PharmaVillage</p>
                            </div>
                        </div>
                    </body>
                    </html>
                    ";




                    // gửi mail link quên mật khẩu one-time use cho người dùng
                    EmailService.SendMail(model.Email, subject, body);            

                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    // troll giả bộ nói là liên kết đặt lại đã được gửi đến email
                    return View("ForgotPasswordConfirmation");
                }
            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return HttpNotFound();
            }

            var model = new ResetPasswordVM
            {
                UserId = userId,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var appDBContext = new AppDBContext();
                var userStore = new AppUserStore(appDBContext);
                var userManager = new AppUserManager(userStore);

                var result = userManager.ResetPassword(model.UserId, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return View("ResetPasswordConfirmation");
                }
                else
                {
                    ModelState.AddModelError("Lỗi", "Đặt lại mật khẩu không thành công.");
                }
            }
            return View(model);
        }
        [Route("DangKi")]
        public ActionResult Register()
        {
            return View();
        }
        [Route("DangKi")]
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
                    if (userManager.IsInRole(user.Id, "Customer"))
                    {

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["Err2"] = "Tài khoản/Mật khẩu không hợp lệ";
                        return this.Login();
                    }
                   
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
        
    }
}