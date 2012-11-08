using System.Linq;
using Domain.Abstract;
using Domain.Purse;
using Domain.Repository;

namespace Domain.Adapter
{
    public class AdapterEFURepoToPrevMod
    {
        private IQueryable<RepositoryOperation> _repository;
        private PreviewModel _model;

        public AdapterEFURepoToPrevMod(IOperationRepository repository, int userID)
        {
            _repository = repository.Repository(userID);

        }
        public PreviewModel GetModel()
        {
            var maxYear = _repository.Max(x => x.Year);
            var minYear = _repository.Min(x => x.Year);
            _model = new PreviewModel(minYear, maxYear);

            foreach (var item in _repository)
            {
                _model.AddDaySpanOperation(item.Year,item.Month,item.Day,new SingleOperation{OperationName = item.OperationName, Value = item.OperationValue,Id = item.ID});
            }
            return _model;
        }
    }
}