using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// Main controller for static pages
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Main page of site
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        /// <summary>
        /// Page about me
        /// </summary>
        /// <returns>View</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Empty at this moment";
            return View();
        }
        /// <summary>
        /// Page of my contatcs
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "My contacts(Мои контакты)";
            return View();
        }
    }
}
