using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;

namespace Web_DrugStore.Controllers
{
    public class ProductController : Controller
    {
        // Khởi tạo DbContext
        DS_DBContext db = new DS_DBContext();
       
        // Hiển thị danh sách tất cả sản phẩm
        public ActionResult AllProducts()
        {
            
           
            List<DanhMuc> danhmuc_left = db.DanhMucs.Where(d => d.DanhMucCha == null ).ToList();
            List<SanPham> sanphams = db.SanPhams.Where(prod => prod.HoatDong == true).ToList();
            ViewBag.ListDanhMuc = danhmuc_left;
            
            return View(sanphams);
        }

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
            var spHienTai = db.SanPhams.FirstOrDefault(sp => sp.SanPhamId == cateId);
            var spLienQuan = db.SanPhams
         .Where(sp => sp.DanhMucId == cateId && sp.SanPhamId != id)
         .ToList();

            ViewBag.ListDanhMuc = spLienQuan;
            return View(spLienQuan);
        }
       
        public ActionResult 


    }
}
