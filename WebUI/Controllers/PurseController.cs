using System.Web.Mvc;
using Domain.Abstract;
using Domain.Adapter;
using Domain.Purse;
using Domain.Repository;

namespace WebUI.Controllers
{
    [Authorize]
    public class PurseController : Controller
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IUserRepository _userRepository;
        private AdapterEFURepoToPrevMod _adapter;
        

        public PurseController(IOperationRepository operationRepository, IUserRepository userRepository)
        {
            _operationRepository = operationRepository;
            _userRepository = userRepository;
        }
        public ViewResult Index()
        {
            _adapter = new AdapterEFURepoToPrevMod(_operationRepository, GetUserID());
            return View(_adapter.GetModel());
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
                    UserName = User.Identity.Name
                });
            if (Request != null && Request.IsAjaxRequest())
                return Json(itemID);
            _adapter = new AdapterEFURepoToPrevMod(_operationRepository, GetUserID());
            return  View("Index",_adapter.GetModel());
        }
        public ActionResult DeleteOperation(int id)
        {
            _operationRepository.RemoveOperation(id, GetUserID());
            _adapter = new AdapterEFURepoToPrevMod(_operationRepository, GetUserID());
            return View();
        }
        public ActionResult ChangeOperation(int id, string newName, int newValue)
        {
            _operationRepository.ChangeOperation(new SingleOperation {Id = id, OperationName = newName, Value = newValue}, GetUserID());
            return View("Index");
        }
        private int GetUserID()
        {
            string userName = User == null ? null : User.Identity.Name;
            return _userRepository.GetUserID(userName);
        }
    }
}
