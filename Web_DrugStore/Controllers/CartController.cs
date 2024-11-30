using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web_DrugStore.Filters;
using Web_DrugStore.Models;
using Web_DrugStore.ViewModel;

namespace Web_DrugStore.Controllers
{
   
    public class CartController : Controller
    {
        // GET: Cart
        DS_DBContext db = new DS_DBContext();


        public ActionResult GioHangMenuPar()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            var tmp = cart;
            if (cart != null)
            {
                return PartialView(tmp.Items);
            }   
            return PartialView(tmp);
        }
        public ActionResult Index()
        {   
            return View();
        }
        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            var tmp = cart;
            return PartialView(tmp);
        }
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart!=null)
            {
                return Json(new { Count = cart.GetTotalQuantity() }, JsonRequestBehavior.AllowGet);
            }    
            return Json(new {Count = 0});

        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1 , Count=0};
            var getProd = db.SanPhams.FirstOrDefault(x => x.SanPhamId == id);
            if (getProd!=null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    SanPhamId = getProd.SanPhamId,
                    sanpham = getProd,
                    SoLuong = quantity,
                    TongTien = quantity * getProd.DonGia
                };
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công", code = 1, Count=1};
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var getProd = cart.Items.FirstOrDefault(x => x.SanPhamId == id);
                if (getProd != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "Xoá sản phẩm khỏi giỏ hàng thành công", code = 1, Count = cart.Items.Count };
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json( new { Success = false});
        }

        // Phần check out

   
        public ActionResult Checkout()
        {

            return View();
        }
        public ActionResult Partial_Item_Checkout()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            var tmp = cart;
            return PartialView(tmp);
        }
    }
}