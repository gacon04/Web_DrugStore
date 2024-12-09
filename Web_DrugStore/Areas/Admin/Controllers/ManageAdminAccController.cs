using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Identity;
using Web_DrugStore.ViewModel;

namespace Web_DrugStore.Areas.Admin.Controllers
{
    [AuthorizationFilter]
    public class ManageAdminAccController : Controller
    {
        // GET: Admin/ManageAdminAc
        
        public ActionResult Index(int? page, string searchText)
        {
            var appDBContext = new AppDBContext();

            var userRole = appDBContext.Roles.FirstOrDefault(r => r.Name == "Admin");

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
    }
}