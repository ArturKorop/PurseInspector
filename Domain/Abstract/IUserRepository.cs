using System.Linq;
using Domain.Purse;
using Domain.Repository;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<UserOperationDataElement> Repository { get; }
        void AddSpanOperation(int year, int month, int day, SingleOperation operation);
        void ChangeSpanOperation(int year, int month, int day, int id, SingleOperation operation);
        void RemoveSpanOperation(int year, int month, int day, int id);

    }
}