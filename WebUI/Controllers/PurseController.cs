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
        //private PreviewModel _previewModel;
        private IUserRepository _repository;
        private AdapterEFURepoToPrevMod _adapter;

        public PurseController(IUserRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            //_previewModel = new PreviewModel(2012, 2013);
            /*      _repository.AddSpanOperation(2012, 1, 5, new SingleOperation { OperationName = "Ticket", Value = 95 });
                    _repository.AddSpanOperation(2012, 1, 5, new SingleOperation { OperationName = "Ticket", Value = 95 });
                    _repository.AddSpanOperation(2012, 1, 5, new SingleOperation { OperationName = "Novus", Value = 124 });
                    _repository.AddSpanOperation(2012, 1, 17, new SingleOperation { OperationName = "Ostin", Value = 44 });
                    _repository.AddSpanOperation(2012, 1, 19, new SingleOperation { OperationName = "Novus", Value = 22 });
                    _repository.AddSpanOperation(2012, 1, 19, new SingleOperation { OperationName = "Ostin", Value = 323 });*/
            _adapter = new AdapterEFURepoToPrevMod(_repository, 1);
            return View(_adapter.GetModel());
        }


        public ActionResult AddOperation(int year,int month,int day,string operationType, string operationName, int operationValue)
        {
            var itemID = _repository.AddOperation(new RepositoryOperation
                {
                    Day = day,
                    Month = month,
                    Year = year,
                    OperationName = operationName,
                    OperationValue = operationValue,
                    OperationType = operationType
                }, 1);
            if (Request.IsAjaxRequest())
                return Json(itemID);

            _adapter = new AdapterEFURepoToPrevMod(_repository, 1);
            return  View("Index",_adapter.GetModel());
        }
        
        public ActionResult DeleteOperation(int id)
        {
            _repository.RemoveOperation(id, 1);
            _adapter = new AdapterEFURepoToPrevMod(_repository, 1);
            return View();
        }
    }
}
