using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Identity;
using Web_DrugStore.Models;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AuthorizationFilter]
    public class AdminOrderController : Controller
    {
        // GET: Admin/Order
        DS_DBContext db = new DS_DBContext();
        public ActionResult Index()
        {
            
            List<DonHang> listdh = db.DonHangs.OrderByDescending(tmp => tmp.NgayDat).ToList();
            return View(listdh);
        }


        public ActionResult GetHoTenById(string id)
        {
            var dbContext = new AppDBContext();
            var userStore = new AppUserStore(dbContext);
            AppUserManager _userManager = new AppUserManager(userStore);
            if (string.IsNullOrEmpty(id))
            {
                return Content("ID không hợp lệ");
            }

            // Lấy thông tin người dùng từ id
            var user = _userManager.FindById(id);
            if (user != null)
            {
                return Content(user.HoTen);
            }

            return Content("Người dùng không tồn tại");
        }
       
    }
}