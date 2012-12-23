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
                var maxDate = _blogRepository.GetBlog().Max(x => x.Date);
                ViewBag.Article = _blogRepository.GetBlog().Single(x => x.Date == maxDate).Text;
            }
            else
            {
                ViewBag.Article = "No one article";
            }
            return View("Index");
        }

        public ActionResult GetArticle(int id)
        {
            ViewBag.Article = _blogRepository.GetBlog().Single(x => x.ID == id).Text;
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
            var temp = article;
            temp.Date = DateTime.Now;
            temp.UserID = GetUserID();
            _blogRepository.AddArticle(article);
            var maxDate = _blogRepository.GetBlog().Max(x => x.Date);
            ViewBag.Article = _blogRepository.GetBlog().Single(x => x.Date == maxDate).Text;
            return View("Index");
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
