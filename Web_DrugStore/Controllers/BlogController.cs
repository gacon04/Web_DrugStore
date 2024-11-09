using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;
using HtmlAgilityPack;
namespace Web_DrugStore.Controllers
{
    public class BlogController : Controller
    {
        DS_DBContext db = new DS_DBContext();
        // GET: Blog

        [Route("GocSucKhoe")]
        public ActionResult Index()
        {

            List<Blog> blogs = db.Blogs.ToList();
            return View(blogs);
        }
        public ActionResult BlogDetail(int id)
        {
           
            var blog = db.Blogs.FirstOrDefault(b => b.Id == id);
           

            return View(blog);
        }
      

        

    }
}