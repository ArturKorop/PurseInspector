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

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ActionResult Index()
        {
            _blogRepository.AddOperation(new Article(){UserID = 1});
            return View();
        }

        public PartialViewResult BlogArchive()
        {
            return PartialView();
        }

    }
}
