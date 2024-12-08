using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.Ajax.Utilities;
using System.Collections;
namespace Web_DrugStore.Controllers
{
    public class ProductController : Controller
    {
        // Khởi tạo DbContext
        DS_DBContext db = new DS_DBContext();
        [Route("TatCaSanPham")]
        // Hiển thị danh sách tất cả sản phẩm
        public ActionResult AllProducts(int? page, int? cateId, string searchText, decimal? minPrice, decimal? maxPrice)
        {
            int Size = 9;
            int PageNumber = (page ?? 1);
            int MaDanhMuc = (cateId ?? 0);
            List<SanPham> sanphams;
            List<DanhMuc> danhmuc_left = db.DanhMucs.Where(d => d.DanhMucCha == null).ToList();

            if (MaDanhMuc != 0)
            {
                sanphams = db.SanPhams
                             .Where(prod => prod.HoatDong == true && prod.DanhMuc.ParentId == MaDanhMuc)
                             .ToList();
            }
            else
            {
                sanphams = db.SanPhams
                             .Where(prod => prod.HoatDong == true)
                             .ToList();
            }

            // Xử lý tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                string lowerSearchText = searchText.ToLower();
                sanphams = sanphams
                             .Where(prod => prod.TenSanPham.ToLower().Contains(lowerSearchText))
                             .ToList();
            }

            if (minPrice.HasValue)
            {
                sanphams = sanphams.Where(prod => prod.DonGia >= minPrice.Value).ToList();
            }
            if (maxPrice.HasValue)
            {
                sanphams = sanphams
                             .Where(prod => prod.DonGia <= maxPrice.Value).ToList();
            }
            ViewBag.ListDanhMuc = danhmuc_left;
            ViewBag.SoLuongBanGhi = sanphams.Count();
            var listProd = sanphams;
            return View(listProd.ToPagedList(PageNumber, Size));
        }

        [Route("ChiTietSanPham{id}")]
        public ActionResult ProdDetail(int id)
        {

            SanPham sp = db.SanPhams.Include("HinhAnhSanPhams").
                                 FirstOrDefault(tmp => tmp.SanPhamId == id);

            if (sp == null)
            {
                return HttpNotFound();
            }

            return View(sp);
        }

        public ActionResult RelatedProd(int cateId, int id)
        {
            
            var spLienQuan = db.SanPhams
         .Where(sp => sp.DanhMucId == cateId && sp.SanPhamId != id)
         .ToList();

            ViewBag.ListDanhMuc = spLienQuan;
            return PartialView(spLienQuan);
        }
       public ActionResult HighRatedProdInDetailRecom(int id)
        {
            List<SanPham> sanphams = db.SanPhams.Where(prod => prod.HoatDong == true && prod.SanPhamId != id).
                OrderByDescending(prod => prod.Rated).Take(5).ToList();
            return PartialView(sanphams);
        }


    }
}
