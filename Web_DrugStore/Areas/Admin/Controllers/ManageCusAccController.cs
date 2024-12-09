using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Models;
using Web_DrugStore.Identity;
using Web_DrugStore.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using PagedList;
using System.Web.UI;
using Microsoft.AspNet.Identity;
namespace Web_DrugStore.Areas.Admin.Controllers
{
    [AuthorizationFilter]
    public class ManageCusAccController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        public ActionResult Index(int? page, string searchText)
        {
            var appDBContext = new AppDBContext();

            var userRole = appDBContext.Roles.FirstOrDefault(r => r.Name == "Customer");

            var userIdsInRole = appDBContext.Set<IdentityUserRole>()
                                            .Where(ur => ur.RoleId == userRole.Id)
                                            .Select(ur => ur.UserId)
                                            .ToList();

            var users = appDBContext.Users
                                    .Where(u => userIdsInRole.Contains(u.Id))
                                    .AsQueryable();

            // Apply search functionality
            if (!string.IsNullOrEmpty(searchText))
            {
                users = users.Where(u => u.Email.Contains(searchText)
                                      || u.UserName.Contains(searchText)
                                      || u.HoTen.Contains(searchText)
                                      || u.SDT.Contains(searchText));
            }
            int pageSize = 10;
            int pageNumber = page ?? 1;
            int totalUsers = users.Count();
            int totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            var pagedUsers = users.OrderBy(u => u.UserName)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            var model = new ManageCusAccVM
            {
                Users = pagedUsers,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                SearchText = searchText
            };

            return View(model);
        }
        public ActionResult AccountDetail(string id, int? orderPage)
        {
            var appDBContext = new AppDBContext();
            var userStore = new AppUserStore(appDBContext);
            var userManager = new AppUserManager(userStore);
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Truy vấn người dùng bằng AppDBContext
            var user = userManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Orderpage = orderPage;
            var model = new AccountDetailVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                HoTen = user.HoTen,
                SDT = user.SDT
            };

            return View(model);
        }
        public ActionResult GetListUserOrder(string id, int? page)
        {
            var dhList = db.DonHangs.AsQueryable();
            int size = 4;
            int pageNumber = (page ?? 1);
            dhList = dhList.Where(sp => sp.UserAspId == id);

            ViewBag.UserId = id;
            dhList = dhList.OrderByDescending(sp => sp.NgayDat);
            var paginatedDH = dhList.ToPagedList(pageNumber, size);
            return PartialView(paginatedDH);
        }
    }
}