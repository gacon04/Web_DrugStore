using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;


namespace Web_DrugStore.Controllers
{
    public class AboutController : Controller
    {
        DS_DBContext _context = new DS_DBContext();

        [Route("LienHe")]
        public ActionResult Contact()
        {
            return View();
        }
        [Route("VePharmaVillage")]
        public ActionResult AboutUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Web_DrugStore.Models.TinNhanLienHe tinnhan)
        {
            if (ModelState.IsValid)
            {
                tinnhan.ThoiGian = DateTime.Now;
                _context.TinNhanLienHes.Add(tinnhan);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "PharmaVillage đã ghi nhận phản hồi của bạn, chúng tôi sẽ hồi đáp sớm nhất!";
                // Chuyển hướng đến trang thành công
                return RedirectToAction("Contact");
            }

            return View(tinnhan);
        }
        public ActionResult Success()
        {
            return View();
        }
        [Route("DichVu")]
        public ActionResult Service()
        {
            return View();
        }
        [Route("DiaDiemMaps")]
        public ActionResult GoogleMapLocations()
        {
            return View();
        }
        [Route("CauHoiThuongGap")]
        public ActionResult FAQ()
        {

            return View();
        }
        // quy chế hđ
        public ActionResult OperatingRegulations()
        {
            return View();
        }
        // chính sách giao hàng
        public ActionResult ShippingPolicy()
        {
            return View();
        }
        // chính sách đổi trả
        public ActionResult ReturnPolicy()
        {
            return View();
        }
        
        // chính sách thanh toán
        public ActionResult PaymentPolicy()
        {
            return View();
        }
        // chính sách quyền riêng tư
        public ActionResult ConfidentialityPolicy()
        {
            return View();
        }

    }
}