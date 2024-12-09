using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Identity;
using Web_DrugStore.Models;
using Web_DrugStore.ViewModel;
namespace Web_DrugStore.Areas.Admin.Controllers
{

    public class DashboardController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        // GET: Admin/Home
        public ActionResult Index()
        {   
            var thangHienTai = DateTime.Now.Month;
            var namHienTai = DateTime.Now.Year;

            var doanhThuTrongThang = db.ChiTietDonHangs
                 .Where(ct => ct.DonHang.NgayDat.Month == thangHienTai && ct.DonHang.NgayDat.Year == namHienTai)
                 .Sum(ct => ct.DonHang.TongTienHang);


            var thangTruoc = thangHienTai == 1 ? 12 : thangHienTai - 1;
            var namTruoc = thangHienTai == 1 ? namHienTai - 1 : namHienTai;

            double doanhThuThangTruoc = db.DonHangs
    .Where(ct => ct.NgayDat.Month == thangTruoc && ct.NgayDat.Year == namTruoc)
    .Select(ct => ct.TongTienHang)
    .DefaultIfEmpty(0)
    .Sum();


            double tangTruongDoanhThu = 0;
            if (doanhThuThangTruoc > 0)
            {
                tangTruongDoanhThu = ((doanhThuTrongThang - doanhThuThangTruoc) / doanhThuThangTruoc) * 100;
            }
            else
            {
                tangTruongDoanhThu = doanhThuTrongThang > 0 ? 100 : 0; // Giả định tăng trưởng 100% nếu trước đó không có doanh thu
            }

            var appDBContext = new AppDBContext();

            var userRole = appDBContext.Roles.FirstOrDefault(r => r.Name == "Customer");

            var userIdsInRole = appDBContext.Set<IdentityUserRole>()
                                            .Where(ur => ur.RoleId == userRole.Id)
                                            .Select(ur => ur.UserId)
                                            .ToList();
            var soLuongKhachHang = userIdsInRole.Count();

            var soLuongDonHang = db.DonHangs
                .Count();

            var soLuongKhachHangMuaTrongThang = db.DonHangs
                .Where(dh => dh.NgayDat.Month == thangHienTai && dh.NgayDat.Year == namHienTai)
                .Select(dh => dh.UserAspId)
                .Distinct()
                .Count();
            
            

            ViewBag.DoanhThuTrongThang = doanhThuTrongThang;
            ViewBag.soLuongKhachHangMuaTrongThang = soLuongKhachHangMuaTrongThang;
            ViewBag.TangTruongDoanhThu = tangTruongDoanhThu;
            ViewBag.SoLuongDonHang = soLuongDonHang;
            ViewBag.SoLuongKhacHang = soLuongKhachHang;
            return View();
        }
        public ActionResult TongDoanhThu()
        {
            return PartialView();
        }
        public ActionResult KhachHangTrongThang()
        {
            return PartialView();
        }
        
        public ActionResult SPBanChay()
        {
            var thangHienTai = DateTime.Now.Month;
            var namHienTai = DateTime.Now.Year;

            // Truy vấn các chi tiết đơn hàng trong tháng hiện tại
            var chiTietDonHangs = db.ChiTietDonHangs
                .Where(ct => ct.DonHang.NgayDat.Month == thangHienTai && ct.DonHang.NgayDat.Year == namHienTai);

            // Thống kê sản phẩm bán chạy
            var thongKeSanPham = chiTietDonHangs
                .GroupBy(ct => ct.SanPhamId)
                .Select(nhom => new
                {
                    SanPhamId = nhom.Key,
                    SoLuongBan = nhom.Sum(ct => ct.SoLuong),
                    DoanhThu = nhom.Sum(ct => ct.SoLuong * ct.DonGia)
                })
                .OrderByDescending(sp => sp.SoLuongBan)
                .Take(10)
                .ToList();

            // Gộp với bảng SanPham để lấy tên và số lượng còn lại
            var sanPhamBanChay = thongKeSanPham
                .Join(
                    db.SanPhams,
                    tk => tk.SanPhamId,
                    sp => sp.SanPhamId, // Điều chỉnh tên khóa chính nếu khác
                    (tk, sp) => new SPBanChayVM
                    {
                        TenSanPham = sp.TenSanPham, // Giả sử có thuộc tính TenSanPham trong SanPham
                        SoLuongBan = tk.SoLuongBan,
                        DoanhThu = (decimal)tk.DoanhThu,
                        SoLuongConLai = sp.SoLuong // Giả sử có thuộc tính SoLuong trong SanPham
                    }
                )
                .ToList();

            return PartialView(sanPhamBanChay);
        }
    
    }
}