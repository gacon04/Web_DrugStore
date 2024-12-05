using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web_DrugStore.Filters;
using Web_DrugStore.Models;
using Web_DrugStore.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using Web_DrugStore.Identity;
using System.Net.Http;
using System.Web.UI.WebControls;

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
        [Route("GioHang")]
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
            if (cart != null)
            {
                return Json(new { Count = cart.GetTotalQuantity() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 });

        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var getProd = db.SanPhams.FirstOrDefault(x => x.SanPhamId == id);
            if (getProd != null)
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
                code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công", code = 1, Count = 1 };
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
            return Json(new { Success = false });
        }
        [AuthenticationFilter]
        public ActionResult Checkout()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Checkout(CheckOutItemVM item)
        {
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    DonHang dh = new DonHang();
                    dh.TenKhachHang = item.TenKhachHang;
                    dh.TenDuong = item.TenDuong;
                    dh.SoNha = item.SoNha;
                    dh.TenHuyen = item.TenHuyen;
                    dh.DiaChiChoLam = item.DiaChiChoLam;
                    dh.TenXa = item.TenXa;
                    dh.TenTinh = item.TenTinh;
                    dh.GhiChu = item.GhiChu;
                    dh.SoDienThoai = item.SoDienThoai;
                    dh.Email = item.Email;
                    string pttt = item.PhuongThucThanhToan;
                    if (pttt.Equals("VNPAY"))
                    {
                        dh.CachThanhToan = HinhThucThanhToan.VNPAY;
                    }
                    else if (pttt.Equals("MOMO"))
                    {
                        dh.CachThanhToan = HinhThucThanhToan.MOMO;
                    }
                    else if (pttt.Equals("COD"))
                    {
                        dh.CachThanhToan = HinhThucThanhToan.COD;
                    }
                    dh.ChiTietDonHangs = new List<ChiTietDonHang>();
                    cart.Items.ForEach(x => dh.ChiTietDonHangs.Add(new ChiTietDonHang
                    {
                        SanPhamId = x.SanPhamId,
                        SoLuong = x.SoLuong,
                        DonGia = (double)x.sanpham.DonGia,
                    }));
                    double tongtienhang = (double)cart.Items.Sum(x => x.TongTien);
                    dh.TongTienHang = tongtienhang;
                    dh.PhiVanChuyen = 20000;
                    dh.NgayDat = DateTime.Now;
                    dh.UserAspId = User.Identity.GetUserId();
                    if (tongtienhang >= 300000)
                    {
                        dh.PhiVanChuyen = 0;
                    }
                    dh.VAT = tongtienhang * 0.1;
                    dh.TongHoaDon = dh.TongTienHang + dh.PhiVanChuyen + dh.VAT;
                    dh.TrangThai = TrangThaiDonHang.ChoXacNhan;
                    db.DonHangs.Add(dh);
                    db.SaveChanges();
                    SendConfirmOrderEmail(dh.DonHangId);
                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess");
                }
                
               
            }
            return View(item);
        }
        public ActionResult SendConfirmOrderEmail(int donHangId)
        {
            var userId = User.Identity.GetUserId();
            // Truy xuất DonHang với ChiTietDonHangs và SanPham
            var dh = db.DonHangs
                       .Include("ChiTietDonHangs.SanPham")
                       .FirstOrDefault(x => x.DonHangId == donHangId);

            if (userId != null)
            {

                var mail = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("nhokljlom99@gmail.com", "hldi aidq yqjy uusr"),
                    EnableSsl = true
                };

                string orderDetails = @"
                    <h3>Chi tiết đơn hàng:</h3>
                    <table style='width: 100%; border-collapse: collapse;'>
                        <thead>
                            <tr style='background-color: #f2f2f2; text-align: left;'>
                                <th style='padding: 8px; border: 1px solid #ddd;'>Sản phẩm</th>
                                <th style='padding: 8px; border: 1px solid #ddd;'>Số lượng</th>
                                <th style='padding: 8px; border: 1px solid #ddd;'>Đơn giá</th>
                            </tr>
                        </thead>
                        <tbody>";

                                foreach (var item in dh.ChiTietDonHangs)
                                {
                                    orderDetails += $@"
                        <tr>
                            <td style='padding: 8px; border: 1px solid #ddd;'>{item.SanPham.TenSanPham}</td>
                            <td style='padding: 8px; border: 1px solid #ddd; text-align: center;'>{item.SoLuong}</td>
                            <td style='padding: 8px; border: 1px solid #ddd; text-align: right;'>{item.DonGia.ToString("N0")} VND</td>
                        </tr>";
                                }

                                orderDetails += @"
                        </tbody>
                    </table>";

                                string emailBody = $@"
                    <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                        <h2 style='color: #4CAF50;'>Xác nhận đơn hàng</h2>
                        <p>Chào {dh.TenKhachHang},</p>
                        <p>Đơn hàng của bạn đã được đặt thành công tại PharmaVillage!</p>
                        {orderDetails}
                        <p>VAT: {dh.VAT.ToString("N0")} VND</p>
                        <p>Phí vận chuyển: {dh.PhiVanChuyen.ToString("N0")} VND</p>
                        <p style='font-weight: bold;'>Tổng tiền: {dh.TongHoaDon.ToString("N0")} VND</p>
                        <p>Cảm ơn bạn đã mua sắm tại cửa hàng của chúng tôi.</p>
                        <p>Trân trọng,</p>
                        <p><b>Đội ngũ hỗ trợ PharmaVillage</b></p>
                    </div>";

                // Tạo đối tượng MailMessage
                var message = new MailMessage
                {
                    From = new MailAddress("nhokljlom99@gmail.com"),
                    Subject = "XÁC NHẬN ĐƠN HÀNG",
                    Body = emailBody,
                    IsBodyHtml = true
                };

                // Gửi đến email người dùng
                message.To.Add(new MailAddress(dh.Email));

                // Gửi email
                mail.Send(message);

                // Chuyển hướng đến trang thành công
                return RedirectToAction("CheckOutSuccess");
            }
            else
            {
                return RedirectToAction("CheckOutFail");
            }
        }



        // hiển thị ra list sản phẩm đang trong giỏ hàng lên phần thanh toán
        public ActionResult Partial_Item_Checkout()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            var tmp = cart;
            return RedirectToAction("Index");
        }
        [AuthenticationFilter]
        public ActionResult CheckOutSuccess()
        {

            return View();
        }
        [AuthenticationFilter]
        public ActionResult CheckOutFail()
        {
            return View();
        }
    }
}
