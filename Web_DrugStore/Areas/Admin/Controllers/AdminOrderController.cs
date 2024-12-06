using Microsoft.AspNet.Identity;
using PagedList;
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
        public ActionResult Index(int? page, string searchText)
        {

            var dhList = db.DonHangs.AsQueryable();
            int size = 9;
            int pageNumber = (page ?? 1);


            if (!string.IsNullOrEmpty(searchText))
            {
                dhList = dhList.Where(sp => sp.MaDonHang.Contains(searchText));
            }
            dhList = dhList.OrderBy(sp => sp.NgayDat);
            var paginatedDH= dhList.ToPagedList(pageNumber, size);

            // Truyền `searchText` để giữ giá trị trong giao diện
            ViewBag.SearchText = searchText;

            return View(paginatedDH);
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
       public ActionResult OrderDetail(int id)
        {

            var donHang = db.DonHangs
                    .Include("ChiTietDonHangs.SanPham")
                    .FirstOrDefault(d => d.DonHangId == id);

            if (donHang == null)
            {
                return HttpNotFound();
            }

            return View(donHang);
        }

        public ActionResult GetListProdInOrder(int id)
        {
            var listCTDH = db.ChiTietDonHangs.Where(ctdh => ctdh.DonHangId == id);
            return PartialView();
        }
    }
}