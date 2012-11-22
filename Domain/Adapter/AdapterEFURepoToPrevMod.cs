using System.Linq;
using Domain.Purse;
using Domain.Repository;

namespace Domain.Adapter
{
    /// <summary>
    /// Adapter for conversion IQueryable to <see cref="PreviewModel"/>
    /// </summary>
    public class AdapterEFURepoToPrevMod 
    {
        private IQueryable<RepositoryOperation> _repository;
        private PreviewModel _model;

        /// <summary>
        /// Constructor for adapter
        /// </summary>
        /// <param name="repository">IQueryable repository for conversion</param>
        public AdapterEFURepoToPrevMod(IQueryable<RepositoryOperation> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Returns <see cref="PreviewModel"/> object
        /// </summary>
        /// <Returns></Returns>
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