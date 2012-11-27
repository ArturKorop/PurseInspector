using System.Linq;
using Domain.Abstract;
using Domain.Adapter;
using Domain.Purse;

namespace Domain.Repository
{
    /// <summary>
    /// Class that implement <see cref="IOperationRepository"/>
    /// </summary>
    public class EFOperationRepository : IOperationRepository
    {
        private readonly EFDbContext _context = new EFDbContext();
        private AdapterEFURepoToPrevMod _adapter;
        private IQueryable<RepositoryOperation> _repository(int userID)
        {
            return _context.RepositoryOperations.Where(x => x.UserID == userID);
        }

        public PreviewModel Model(int userID)
        {
            _adapter = new AdapterEFURepoToPrevMod(_repository(userID));
            return _adapter.GetModel();
        }
        public int AddOperation(RepositoryOperation repositoryOperation)
        {
            var operation = new RepositoryOperation
                {
                    Day = repositoryOperation.Day,
                    Month = repositoryOperation.Month,
                    Year = repositoryOperation.Year,
                    OperationName = repositoryOperation.OperationName,
                    OperationValue = repositoryOperation.OperationValue,
                    OperationType = repositoryOperation.OperationType,
                    UserName = repositoryOperation.UserName,
                    UserID = repositoryOperation.UserID
                };
            _context.RepositoryOperations.Add(operation);
            _context.SaveChanges();
            return _context.RepositoryOperations.Where(y => y.UserID == repositoryOperation.UserID).Max(x => x.ID);
        }
        public void ChangeOperation(SingleOperation operation, int userID)
        {
            var item = _context.RepositoryOperations.FirstOrDefault(x => x.ID == operation.Id && x.UserID == userID);
            if (item != null)
            {
                item.OperationName = operation.OperationName;
                item.OperationValue = operation.Value;
            }
            _context.SaveChanges();
        }
        public void RemoveOperation(int id, int userID)
        {
            var temp = _context.RepositoryOperations.FirstOrDefault(x => x.ID == id && x.UserID == userID);
            if(temp != null)
                _context.RepositoryOperations.Remove(temp);
            _context.SaveChanges();
        }
    }
}