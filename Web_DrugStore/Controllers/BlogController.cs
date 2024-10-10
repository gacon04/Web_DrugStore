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
           
            var blog = db.Blogs.FirstOrDefault(b => b.Id == id);// lấy bài viết từ cơ sở dữ liệu bằng id
            if (blog != null)
            {
                // Phân tích HTML và lấy nội dung trong thẻ body
                var doc = new HtmlDocument();

                doc.LoadHtml(HttpUtility.HtmlDecode(blog.NoiDung));

                var bodyNode = doc.DocumentNode.SelectSingleNode("//body");
                blog.NoiDung = bodyNode != null ? bodyNode.InnerHtml : string.Empty;
            }

            return View(blog);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.NoiDung = HttpUtility.HtmlEncode(blog.NoiDung);
                blog.IdTacGia = 1;
                blog.CreatedAt = DateTime.Now;
                blog.UpdatedAt = DateTime.Now;
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

    }
}