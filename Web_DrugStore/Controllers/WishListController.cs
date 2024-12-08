using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;

namespace Web_DrugStore.Controllers
{
    
    public class WishListController : Controller
    {
        private DS_DBContext db = new DS_DBContext();

        // GET: WishList
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var wishlist = db.DanhSachYeuThichs
                             .FirstOrDefault(w => w.UserAspId == userId);

            var wishlistItems = new List<SanPham>();

            if (wishlist != null)
            {
                wishlistItems = wishlist.DanhSachSanPham.ToList();
            }

            return View(wishlistItems);
        }

        [HttpPost]
        public ActionResult AddToWishList(int id)
        {
            var result = new { Success = false, Message = "", Count = 0 };
            var userId = User.Identity.GetUserId();

            var product = db.SanPhams.Find(id);
            if (product == null)
            {
                result = new { Success = false, Message = "Sản phẩm không tồn tại.", Count = 0 };
                return Json(result);
            }

            var wishlist = db.DanhSachYeuThichs.FirstOrDefault(w => w.UserAspId == userId);
            if (wishlist == null)
            {
                wishlist = new DanhSachYeuThich
                {
                    UserAspId = userId,
                    DanhSachSanPham = new List<SanPham>()
                };
                db.DanhSachYeuThichs.Add(wishlist);
            }

            if (!wishlist.DanhSachSanPham.Contains(product))
            {
                wishlist.DanhSachSanPham.Add(product);
                db.SaveChanges();
                result = new { Success = true, Message = "Thêm sản phẩm vào danh sách yêu thích thành công.", Count = wishlist.DanhSachSanPham.Count };
            }
            else
            {
                result = new { Success = false, Message = "Sản phẩm đã có trong danh sách yêu thích.", Count = wishlist.DanhSachSanPham.Count };
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult RemoveFromWishList(int id)
        {
            var result = new { Success = false, Message = "", Count = 0 };
            var userId = User.Identity.GetUserId();

            var wishlist = db.DanhSachYeuThichs.FirstOrDefault(w => w.UserAspId == userId);
            if (wishlist != null)
            {
                var product = wishlist.DanhSachSanPham.FirstOrDefault(p => p.SanPhamId == id);
                if (product != null)
                {
                    wishlist.DanhSachSanPham.Remove(product);
                    db.SaveChanges();
                    result = new { Success = true, Message = "Xóa sản phẩm khỏi danh sách yêu thích thành công.", Count = wishlist.DanhSachSanPham.Count };
                }
                else
                {
                    result = new { Success = false, Message = "Sản phẩm không có trong danh sách yêu thích.", Count = wishlist.DanhSachSanPham.Count };
                }
            }
            else
            {
                result = new { Success = false, Message = "Danh sách yêu thích không tồn tại.", Count = 0 };
            }

            return Json(result);
        }

        public ActionResult Partial_WishListItems()
        {
            var userId = User.Identity.GetUserId();
            var wishlistItems = new List<SanPham>();

            var wishlist = db.DanhSachYeuThichs.FirstOrDefault(w => w.UserAspId == userId);
            if (wishlist != null)
            {
                wishlistItems = wishlist.DanhSachSanPham.ToList();
            }

            return PartialView(wishlistItems);
        }
    

}
}