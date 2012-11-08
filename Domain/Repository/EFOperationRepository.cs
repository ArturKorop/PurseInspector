using System.Linq;
using Domain.Abstract;
using Domain.Purse;

namespace Domain.Repository
{
    public class EFOperationRepository : IOperationRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<RepositoryOperation> Repository(int userID)
        {
            return _context.RepositoryOperations.Where(x => x.UserID == userID);
        }

        public int AddOperation(RepositoryOperation repositoryOperation, int userID)
        {
            var operation = new RepositoryOperation
                {
                    Day = repositoryOperation.Day,
                    Month = repositoryOperation.Month,
                    Year = repositoryOperation.Year,
                    OperationName = repositoryOperation.OperationName,
                    OperationValue = repositoryOperation.OperationValue,
                    OperationType = repositoryOperation.OperationName,
                    UserName = "akorop",
                    UserID = userID
                };
            _context.RepositoryOperations.Add(operation);
            _context.SaveChanges();
            return _context.RepositoryOperations.Where(y => y.UserID == userID).Max(x => x.ID);
        }

        public void ChangeOperation(int id, SingleOperation operation, int userID)
        {
        }

        public void RemoveOperation(int id, int userID)
        {
            var temp = _context.RepositoryOperations.FirstOrDefault(x => x.ID == id && x.UserID == userID);
            _context.RepositoryOperations.Remove(temp);
            _context.SaveChanges();
        }
    }
}