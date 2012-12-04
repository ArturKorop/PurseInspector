using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class UserInformationController : Controller
    {
        public PartialViewResult UserInfo()
        {
            string date = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture) + " " + DateTime.Now.Month.ToString(CultureInfo.InvariantCulture) + " " + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            return PartialView("UserInfo",date);
        }

    }
}
