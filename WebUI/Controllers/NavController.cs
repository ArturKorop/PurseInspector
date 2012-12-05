using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    /// <summary>
    /// Controller for navigation menu of site
    /// </summary>
    public class NavController : Controller
    {
        /// <summary>
        /// Return a partial view, where presents all page of site, which I'll would show
        /// </summary>
        /// <param name="menuItem">Selected page of menu</param>
        /// <returns>Partial view of menu</returns>
        public PartialViewResult Menu(string menuItem = null)
        {
            ViewBag.SelectedMenuItem = menuItem;
            var listMenuItem = new List<MenuLinkModels>
                {
                    new MenuLinkModels {Name = "Home", Controller = "Home", Action = "Index"},
                    new MenuLinkModels {Name = "Purse", Controller = "Purse", Action = "Index"},
                    new MenuLinkModels {Name = "YearPurse", Controller = "Purse", Action = "ViewYear"},
                    new MenuLinkModels {Name = "About", Controller = "Home", Action = "About"},
                    new MenuLinkModels {Name = "Contact", Controller = "Home", Action = "Contact"}
                };
            return PartialView(listMenuItem);
        }

    }
}
