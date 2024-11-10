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

            List<Blog> blogs = db.Blogs.Where(bl => bl.HienThi == true).ToList();
            return View(blogs);
        }
        public ActionResult BlogDetail(int id)
        {
           
            Blog blog = db.Blogs.FirstOrDefault(b => b.Id == id);
            

            return View(blog);
        }
        public ActionResult NewestBlog()
        {
            List<Blog> blogs = db.Blogs.Where(bl => bl.HienThi == true ).OrderByDescending(bl => bl.UpdatedAt).Take(5).ToList();
            return PartialView(blogs);
        }
        public ActionResult RelatedBlog(int cateId,int thisBlogId)
        {
            List<Blog> blogs = db.Blogs.Where(bl => bl.HienThi == true && bl.DanhMucBlogId == cateId && bl.Id!=thisBlogId).Take(2).ToList();
            return PartialView(blogs);
        }




    }
}