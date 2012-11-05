using System.Linq;
using Domain.Abstract;
using Domain.Purse;
using Domain.Repository;

namespace Domain.Adapter
{
    public class EFUserRepositoryToPurseSingleUserModel
    {
        private IQueryable<UserOperationDataElement> _repository;
        private PurseSingleUserModel _model;

        public EFUserRepositoryToPurseSingleUserModel(IUserRepository repository)
        {
            _repository = repository.Repository;

        }
        public PurseSingleUserModel GetModel()
        {
            var maxYear = _repository.Max(x => x.Year);
            var minYear = _repository.Min(x => x.Year);
            _model = new PurseSingleUserModel(minYear, maxYear);

            foreach (var item in _repository)
            {
                _model.AddDaySpanOperation(item.Year,item.Month,item.Day,new SingleOperation{OperationName = item.OperationName, Value = item.OperationValue});
            }
            return _model;
        }
    }
}