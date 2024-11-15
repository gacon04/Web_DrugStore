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
using Web_DrugStore.Filters;
namespace Web_DrugStore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
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
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Lỗi", "Dữ liệu nhập vào không hợp lệ");
                return View();
            }
            return View();
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
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.FindByEmail(tk);
            if (user != null && userManager.CheckPassword(user, mk))
            {
                var authenManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties(), userIdentity);
                if (userManager.IsInRole(user.Id,"Admin"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }    
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Lỗi", "Tài khoản và mật khẩu nhập vào không hợp lệ.");
            }
            return View();
        }

        [AuthenticationFilter]
        public ActionResult WishList()
        {
            return View();
        }
        
        [AuthenticationFilter]
        public ActionResult Checkout()
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