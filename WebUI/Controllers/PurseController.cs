using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Purse;

namespace WebUI.Controllers
{
    public class PurseController : Controller
    {
        private PurseSingleUserModel _purseSingleUserModel;
        public ActionResult Index()
        {
            _purseSingleUserModel = new PurseSingleUserModel(2012,2013);
            _purseSingleUserModel.Years[0].Months[1].Days[5].AddSpanOperation(new SingleOperation{OperationName = "Ticket",Value = 95});
            _purseSingleUserModel.Years[0].Months[1].Days[5].AddSpanOperation(new SingleOperation { OperationName = "Novus", Value = 124 });
            _purseSingleUserModel.Years[0].Months[1].Days[5].AddSpanOperation(new SingleOperation { OperationName = "Ostin", Value = 900 });
            _purseSingleUserModel.Years[0].Months[1].Days[12].AddSpanOperation(new SingleOperation { OperationName = "Novus", Value = 66 });
            _purseSingleUserModel.Years[0].Months[1].Days[17].AddSpanOperation(new SingleOperation { OperationName = "Ostin", Value = 344 });
            return View(_purseSingleUserModel);
        }

        public ActionResult AddDaySpanOperation(int year,int month,int day,string operationName, int operationValue)
        {
            _purseSingleUserModel.AddDaySpanOperation(year,month,day,new SingleOperation{OperationName = operationName,Value = operationValue});
            return RedirectToAction("Index");
        }
    }
}
