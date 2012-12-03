using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        public PartialViewResult Menu(string menuItem = null)
        {
            ViewBag.SelectedMenuItem = menuItem;
            var listMenuItem = new List<MenuLinkModels>();
            listMenuItem.Add(new MenuLinkModels { Name = "Home", Controller = "Home" , Action = "Index"});
            listMenuItem.Add(new MenuLinkModels { Name = "Purse", Controller = "Purse", Action = "Index" });
            listMenuItem.Add(new MenuLinkModels { Name = "Year", Controller = "Purse", Action = "ViewYear" });
            listMenuItem.Add(new MenuLinkModels { Name = "About", Controller = "Home", Action = "About" });
            listMenuItem.Add(new MenuLinkModels { Name = "Contact", Controller = "Home", Action = "Contact" });
            return PartialView(listMenuItem);
        }

    }
}
