using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Blog;
using Domain.Repository;

namespace WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;

        public BlogController(IBlogRepository blogRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            if (_blogRepository.GetBlog().Count > 0)
            {
                try
                {
                    ViewBag.Article = new HtmlString(_blogRepository.LastArticle.Text.Replace("\n", "<br>"));
                }
                catch (Exception)
                {}
            }
            else
            {
                ViewBag.Article = "No one article";
            }
            return View("Index");
        }

        public ActionResult GetArticle(int id)
        {
            ViewBag.Article = new HtmlString(_blogRepository.GetArticle(id).Text.Replace("\n", "<br>"));
            return View("Index");
        }

        public PartialViewResult BlogArchive()
        {
            var temp = _blogRepository.GetBlogMap();
            return PartialView(temp);
        }

        public ActionResult AddArticle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddArticle(Article article)
        {
            Article temp;
            temp = article.ID != 0 ? _blogRepository.GetArticle(article.ID) : new Article();
            temp.Text = article.Text;
            temp.ArticleShortName = article.ArticleShortName;
            temp.Date = DateTime.Now;
            temp.UserID = GetUserID();
            _blogRepository.AddArticle(temp);
            ViewBag.Article = new HtmlString(_blogRepository.LastArticle.Text.Replace("\n", "<br>"));
            return View("Index");
        }
        public ActionResult DeleteArticle(int id)
        {
            _blogRepository.DeleteArticle(id, GetUserID());
            ViewBag.Article = new HtmlString(_blogRepository.LastArticle.Text.Replace("\n", "<br>"));
            return View("Index");
        }
        public ActionResult EditArticle(int id)
        {
            var edit = _blogRepository.GetArticle(id);
            return View("AddArticle", edit);
        }

        private int GetUserID()
        {
            string userName = User == null ? null : User.Identity.Name;
            if (userName == null)
                return 0;
            return _userRepository.GetUserID(userName);
        }  

    }
}
