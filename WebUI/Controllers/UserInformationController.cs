using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    /// <summary>
    /// Controller for widget for user info
    /// </summary>
    public class UserInformationController : Controller
    {
        /// <summary>
        /// Action, that return partial view of user information
        /// </summary>
        /// <returns>Partial view of user information or other information</returns>
        public PartialViewResult UserInfo()
        {
            string date = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture) + " " + DateTime.Now.Month.ToString(CultureInfo.InvariantCulture) + " " + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            return PartialView("UserInfo",date);
        }
    }
}
