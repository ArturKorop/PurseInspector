using System.Linq;
using Domain.Purse;
using Domain.Repository;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<RepositoryOperation> Repository(int userID);
        int AddOperation(RepositoryOperation repositoryOperation, int userID);
        void ChangeOperation(int id, SingleOperation operation, int userID);
        void RemoveOperation(int id, int userID);

    }
}