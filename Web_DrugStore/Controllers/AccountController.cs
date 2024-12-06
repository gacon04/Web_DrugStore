﻿using Microsoft.AspNet.Identity;
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
    }
}