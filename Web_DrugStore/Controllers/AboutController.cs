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

                TempData["SuccessMessage"] = "PharmaVillage ghi nhận phản hồi!";
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
    }
}