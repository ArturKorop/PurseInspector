using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Adapter;
using Domain.Purse;
using Domain.Repository;

namespace WebUI.Controllers
{
    public class PurseController : Controller
    {
        private PurseSingleUserModel _purseSingleUserModel;
        private IUserRepository _repository;

        public PurseController(IUserRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            //_purseSingleUserModel = new PurseSingleUserModel(2012, 2013);
            /*      _repository.AddSpanOperation(2012, 1, 5, new SingleOperation { OperationName = "Ticket", Value = 95 });
                    _repository.AddSpanOperation(2012, 1, 5, new SingleOperation { OperationName = "Ticket", Value = 95 });
                    _repository.AddSpanOperation(2012, 1, 5, new SingleOperation { OperationName = "Novus", Value = 124 });
                    _repository.AddSpanOperation(2012, 1, 17, new SingleOperation { OperationName = "Ostin", Value = 44 });
                    _repository.AddSpanOperation(2012, 1, 19, new SingleOperation { OperationName = "Novus", Value = 22 });
                    _repository.AddSpanOperation(2012, 1, 19, new SingleOperation { OperationName = "Ostin", Value = 323 });*/
            EFUserRepositoryToPurseSingleUserModel adapter = new EFUserRepositoryToPurseSingleUserModel(_repository);
            _purseSingleUserModel = adapter.GetModel();

            return View(_purseSingleUserModel);
        }


        public ActionResult AddDaySpanOperation(int year,int month,int day,string operationName, int operationValue)
        {
            _repository.AddSpanOperation(year, month, day, new SingleOperation { OperationName = operationName, Value = operationValue });
            return RedirectToAction("Index");
        }
    }
}
