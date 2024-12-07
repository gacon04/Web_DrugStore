using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Identity;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    
    
    public class UserAccountController : Controller
    {
        [AuthorizationFilter]
        // GET: Admin/UserAccount
        public ActionResult Index()
        {
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
            if (user != null)
            {
                if (userManager.CheckPassword(user, mk))
                {
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties(), userIdentity);
                    if (userManager.IsInRole(user.Id, "Admin"))
                    {

                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
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
        [AuthorizationFilter]
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Login", "UserAccount");
        }
    }
}