using System;
using System.Collections.Generic;
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

            List<SanPham> sanphams = db.SanPhams.ToList();
            List<DanhMuc> danhmuc_left = db.DanhMucs.Where(d => d.DanhMucCha == null).ToList();
            ViewBag.ListDanhMuc = danhmuc_left;
            return View(sanphams);
        }

        public ActionResult ProdDetail(int id)
        {
            return View();
        }
        // Hiển thị form tạo sản phẩm

        
        
    }
}
