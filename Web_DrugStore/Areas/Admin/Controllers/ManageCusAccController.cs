using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Models;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [AuthorizationFilter]
    public class ManageCusAccController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        // GET: Admin/CustomerAccount
        public ActionResult Index(int? page, string searchText)
        {
            int size = 10;
            int pageNumber = (page ?? 1);

            var taikhoan = db.SanPhams.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                taikhoan = taikhoan.Where(sp => sp.TenSanPham.Contains(searchText));
            }
            taikhoan = taikhoan.OrderBy(sp => sp.NgayTao);
            var paginatedSanpham = sanpham.ToPagedList(pageNumber, size);

            // Truyền `searchText` để giữ giá trị trong giao diện
            ViewBag.SearchText = searchText;

            return View(paginatedSanpham);
        }
    }
}