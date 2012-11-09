using System.Linq;
using Domain.Purse;
using Domain.Repository;

namespace Domain.Adapter
{
    public class AdapterEFURepoToPrevMod
    {
        private IQueryable<RepositoryOperation> _repository;
        private PreviewModel _model;

        public AdapterEFURepoToPrevMod(IQueryable<RepositoryOperation> repository)
        {
            _repository = repository;
        }
        public PreviewModel GetModel()
        {
            _model = new PreviewModel();

            foreach (var item in _repository)
            {
                _model.AddDaySpanOperation(item.Year,item.Month,item.Day,new SingleOperation{OperationName = item.OperationName, Value = item.OperationValue,Id = item.ID});
            }
            return _model;
        }
    }
}