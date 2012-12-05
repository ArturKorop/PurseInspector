using System;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Purse;
using Domain.Repository;

namespace WebUI.Controllers
{
    /// <summary>
    /// Controller for purse and statistics of money operations
    /// </summary>
    public class PurseController : Controller
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Init controller for purse(money statistic)
        /// </summary>
        /// <param name="operationRepository">Object for connection to table of operation in database</param>
        /// <param name="userRepository">Object for connection to table of user information in database</param>
        public PurseController(IOperationRepository operationRepository, IUserRepository userRepository)
        {
            _operationRepository = operationRepository;
            _userRepository = userRepository;
        }
        /// <summary>
        /// Action, that retur View of current month purse
        /// </summary>
        /// <returns>View</returns>
        public ViewResult Index()
        {
            return View(_operationRepository.Model(GetUserID()).CurrentMonth());
        }
        /// <summary>
        /// Action, that return fixed month purse
        /// </summary>
        /// <param name="currentMonth">Month number</param>
        /// <param name="currentYear">Year number</param>
        /// <returns></returns>
        public ViewResult GetMonth(int currentMonth, int currentYear)
        {
            var temp = _operationRepository.Model(GetUserID());
            temp.SetCurrentMonth(currentMonth, currentYear);
            return View("Index",temp.CurrentMonth());
        }
        /// <summary>
        /// Action, that add a new operation to database
        /// </summary>
        /// <param name="year">Year of operation</param>
        /// <param name="month">Month of operation</param>
        /// <param name="day">Date of operation</param>
        /// <param name="operationType">Type of operation</param>
        /// <param name="operationName">Name of operation</param>
        /// <param name="operationValue">Value of operation</param>
        /// <returns>If it is ajax request - return Json data of ID's that operation</returns>
        public ActionResult AddOperation(int year,int month,int day,string operationType, string operationName, int operationValue)
        {
            var itemID = _operationRepository.AddOperation(new RepositoryOperation
                {
                    Day = day,
                    Month = month,
                    Year = year,
                    OperationName = operationName,
                    OperationValue = operationValue,
                    OperationType = operationType,
                    UserID = GetUserID(),
                    UserName = _userRepository.GetUserName(GetUserID())
                });
            if (Request != null && Request.IsAjaxRequest())
                return Json(itemID);
            return View("Index", _operationRepository.Model(GetUserID()).CurrentMonth());
        }
        /// <summary>
        /// Action, that delete operation from datebase
        /// </summary>
        /// <param name="id">ID of daleted operation</param>
        /// <returns>If it is ajax request - not return</returns>
        public ActionResult DeleteOperation(int id)
        {
            _operationRepository.RemoveOperation(id, GetUserID());
            return View();
        }
        /// <summary>
        /// Action, that change exists operation
        /// </summary>
        /// <param name="id">ID of operation</param>
        /// <param name="newName">New name of opeartion</param>
        /// <param name="newValue">New value of operation</param>
        /// <returns>If it is ajax request - not return</returns>
        public ActionResult ChangeOperation(int id, string newName, int newValue)
        {
            _operationRepository.ChangeOperation(new SingleOperation {Id = id, OperationName = newName, Value = newValue}, GetUserID());
            return View("Index", _operationRepository.Model(GetUserID()).CurrentMonth());
        }
        /// <summary>
        /// Action, that return a next month
        /// </summary>
        /// <param name="currentMonth">Current month's name</param>
        /// <param name="currentYear">Current year's name</param>
        /// <returns>If it is ajax request - return Json data of next month</returns>
        public ActionResult NextMonth(int currentMonth, int currentYear)
        {
            if (Request != null && Request.IsAjaxRequest())
            {
                var temp = _operationRepository.Model(GetUserID()).NextMonth(currentMonth, currentYear).ToJSON();
                return Json(temp);
            }
            return View("Index", _operationRepository.Model(GetUserID()).NextMonth(currentMonth, currentYear));
        }
        /// <summary>
        /// Action, that return a prev month
        /// </summary>
        /// <param name="currentMonth">Current month's name</param>
        /// <param name="currentYear">Current year's name</param>
        /// <returns>If it is ajax request - return Json data of prev month</returns>
        public ActionResult PrevMonth(int currentMonth, int currentYear)
        {
            if (Request != null && Request.IsAjaxRequest())
            {
                var temp = _operationRepository.Model(GetUserID()).PrevMonth(currentMonth, currentYear).ToJSON();
                return Json(temp);
            }
            return View("Index", _operationRepository.Model(GetUserID()).PrevMonth(currentMonth, currentYear));
        }
        /// <summary>
        /// Action, that return statistics of span operation for month
        /// </summary>
        /// <param name="currentMonth">Current month's name</param>
        /// <param name="currentYear">Current year's name</param>
        /// <returns>If it is ajax request - return Json data of statistics</returns>
        public ActionResult SpanStatistics(int currentMonth, int currentYear)
        {
            var temp = _operationRepository.Model(GetUserID());
            temp.SetCurrentMonth(currentMonth, currentYear);
            if (Request != null && Request.IsAjaxRequest())
            {
                return Json(temp.CurrentMonth().SpanStatistics());
            }
            return View("Index", _operationRepository.Model(GetUserID()).CurrentMonth());
        }
        /// <summary>
        /// Action, that return statistics of span operation for year
        /// </summary>
        /// <param name="currentYear">Current year's name</param>
        /// <returns>If it is ajax request - return Json data of statistics</returns>
        public ActionResult YearSpanStatistics(int currentYear)
        {
            var temp = _operationRepository.Model(GetUserID()).GetYear(currentYear);
            if (Request != null && Request.IsAjaxRequest())
            {
                return Json(temp.YearSpanStatistics());
            }
            return View("ViewYear", _operationRepository.Model(GetUserID()).GetYear(DateTime.Now.Year));
        }
        /// <summary>
        /// Action, that return an autocompleate tags for current user
        /// </summary>
        /// <returns>If it is ajax request - return Json data of autocompleate tags</returns>
        public ActionResult GetAutocompleteTags()
        {
            if (Request != null && !Request.IsAjaxRequest())
            {
                return View("Index", _operationRepository.Model(GetUserID()).CurrentMonth());
            }
            return Json(_operationRepository.Model(GetUserID()).GetAutocompleteTags());
        }
        /// <summary>
        /// Action, that return a View for year
        /// </summary>
        /// <param name="currentYear">Current year's name</param>
        /// <returns>View of year</returns>
        public ActionResult ViewYear(int? currentYear)
        {
            if (currentYear == null)
                currentYear = DateTime.Now.Year;
            return View(_operationRepository.Model(GetUserID()).GetYear((int)currentYear));
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
