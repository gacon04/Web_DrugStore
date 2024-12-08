using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Filters;
using Web_DrugStore.Models;
using Web_DrugStore.Identity;
using Web_DrugStore.ViewModel;
namespace Web_DrugStore.Areas.Admin.Controllers
{
    [AuthorizationFilter]
    public class ManageCusAccController : Controller
    {
                DS_DBContext db = new DS_DBContext();
                public ActionResult Index(int? page, string searchText)
                {
                    var appDBContext = new AppDBContext();
                    var userStore = new AppUserStore(appDBContext);
                    var userManager = new AppUserManager(userStore);

                    var users = userManager.Users.AsQueryable();

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        users = users.Where(u => u.Email.Contains(searchText)
                                              || u.UserName.Contains(searchText)
                                              || u.HoTen.Contains(searchText)
                                              || u.SDT.Contains(searchText));
                    }

                    // Phân trang
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