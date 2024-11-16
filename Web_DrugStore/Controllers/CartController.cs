using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Models;

namespace Web_DrugStore.Controllers
{
   
    public class CartController : Controller
    {
        // GET: Cart
        DS_DBContext db = new DS_DBContext();
        public ActionResult MyCart()
        {
            List<CTGH> lstGioHang = LayGioHang();
            
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangMenuPar()
        {
            return PartialView();
        }
        public class CTGH
        {
            public int SanPhamId { get; set; }
            public string TenSanPham { get; set; }
            public int SoLuong { get; set; }
            public double Gia { get; set; }
            public string HinhAnh { get; set; }
            public double ThanhTien
            {
                get { return SoLuong * Gia; }
            }
        }
        public CTGH TaoCTGH(int masp)
        {
            CTGH tmp = new CTGH();
            tmp.SanPhamId = masp;
            SanPham sp = db.SanPhams.FirstOrDefault(n => n.SanPhamId == masp);
            tmp.HinhAnh = sp.Thumbnail;
            tmp.SoLuong = 1;
            tmp.Gia = (double)sp.DonGia;
            tmp.TenSanPham = sp.TenSanPham;
            return tmp;

        }
        public List<CTGH> LayGioHang()
        {
            List<CTGH> lstGioHang = Session["GioHang"] as List<CTGH>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<CTGH>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int id, string url)
        {
            //Lấy giỏ hàng hiện tại
            List<CTGH> lstCart = LayGioHang();
            //Kiểm tra nếu sản phẩm chưa có trong giỏ thì thêm vào, nếu đã có thì tăng số lượng
            CTGH chitiet = lstCart.Find(n => n.SanPhamId  == id);
            if (chitiet == null)
            {
                chitiet = TaoCTGH(id);
                lstCart.Add(chitiet);
            }
            else
            {
                chitiet.SoLuong++;
            }
            return Redirect(url);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<CTGH> lstGioHang = Session["GioHang"] as List<CTGH>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.SoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<CTGH> lstGioHang = Session["GioHang"] as List<CTGH>;
            if (lstGioHang != null)
            {
                dTongTien = (double)lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }



    }
}