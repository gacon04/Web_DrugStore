using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_DrugStore.Models;
using HtmlAgilityPack;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
namespace Web_DrugStore.Controllers
{
    public class BlogController : Controller
    {
        private DS_DBContext db = new DS_DBContext();

        

        [Route("GocSucKhoe")]
        public ActionResult Index(int? page)
        {
            int Size = 9; 
            int PageNumber = (page ?? 1);
            List<Blog> blogs = db.Blogs.Where(bl => bl.HienThi == true).ToList();
            var listBlogs = blogs;
            return View(listBlogs.ToPagedList(PageNumber, Size));
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