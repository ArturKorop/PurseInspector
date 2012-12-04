﻿using System;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Purse;
using Domain.Repository;

namespace WebUI.Controllers
{
    public class PurseController : Controller
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IUserRepository _userRepository;
        
        public PurseController(IOperationRepository operationRepository, IUserRepository userRepository)
        {
            _operationRepository = operationRepository;
            _userRepository = userRepository;
        }
        public ViewResult Index()
        {
            return View(_operationRepository.Model(GetUserID()).CurrentMonth());
        }
        public ViewResult GetMonth(int currentMonth, int currentYear)
        {
            var temp = _operationRepository.Model(GetUserID());
            temp.SetCurrentMonth(currentMonth, currentYear);
            return View("Index",temp.CurrentMonth());
        }
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
        public ActionResult DeleteOperation(int id)
        {
            _operationRepository.RemoveOperation(id, GetUserID());
            return View();
        }
        public ActionResult ChangeOperation(int id, string newName, int newValue)
        {
            _operationRepository.ChangeOperation(new SingleOperation {Id = id, OperationName = newName, Value = newValue}, GetUserID());
            return View("Index", _operationRepository.Model(GetUserID()).CurrentMonth());
        }
        public ActionResult NextMonth(int currentMonth, int currentYear)
        {
            if (Request != null && Request.IsAjaxRequest())
            {
                var temp = _operationRepository.Model(GetUserID()).NextMonth(currentMonth, currentYear).ToJSON();
                return Json(temp);
            }
            return View("Index", _operationRepository.Model(GetUserID()).NextMonth(currentMonth, currentYear));
        }
        public ActionResult PrevMonth(int currentMonth, int currentYear)
        {
            if (Request != null && Request.IsAjaxRequest())
            {
                var temp = _operationRepository.Model(GetUserID()).PrevMonth(currentMonth, currentYear).ToJSON();
                return Json(temp);
            }
            return View("Index", _operationRepository.Model(GetUserID()).PrevMonth(currentMonth, currentYear));
        }
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
        public ActionResult YearSpanStatistics(int currentYear)
        {
            var temp = _operationRepository.Model(GetUserID()).GetYear(currentYear);
            if (Request != null && Request.IsAjaxRequest())
            {
                return Json(temp.YearSpanStatistics());
            }
            return View("ViewYear", _operationRepository.Model(GetUserID()).GetYear(DateTime.Now.Year));
        }
        public ActionResult GetAutocompleteTags()
        {
            if (!Request.IsAjaxRequest())
            {
                return View("Index", _operationRepository.Model(GetUserID()).CurrentMonth());
            }
            return Json(_operationRepository.Model(GetUserID()).GetAutocompleteTags());
        }
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
