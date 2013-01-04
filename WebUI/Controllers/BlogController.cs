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
                    ViewBag.Article = _blogRepository.LastArticle.Text;
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
            ViewBag.Article = _blogRepository.GetArticle(id).Text;
            return View("Index");
        }

        public PartialViewResult BlogArchive()
        {
            var temp = _blogRepository.GetBlogMap();
            return PartialView(temp);
        }

        public ActionResult SaveArticle()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveArticle(Article article)
        {
            Article temp;
            temp = article.ID != 0 ? _blogRepository.GetArticle(article.ID) : new Article();
            temp.Text = article.Text;
            temp.ArticleShortName = article.ArticleShortName;
            temp.Date = DateTime.Now;
            temp.UserID = GetUserID();
            _blogRepository.AddArticle(temp);
            ViewBag.Article = _blogRepository.LastArticle.Text;
            return RedirectToAction("Index");
        }
        public ActionResult DeleteArticle(int id)
        {
            _blogRepository.DeleteArticle(id, GetUserID());
            if(_blogRepository.GetBlog().Count() != 0)
                ViewBag.Article = _blogRepository.LastArticle.Text;
            else
                ViewBag.Article = "No one article";
            return RedirectToAction("Index");
        }
        public ActionResult EditArticle(int id)
        {
            var edit = _blogRepository.GetArticle(id);
            return View("SaveArticle", edit);
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
