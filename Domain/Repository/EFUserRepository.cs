using System.Linq;
using Domain.Abstract;
using Domain.Purse;

namespace Domain.Repository
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<UserOperationDataElement> Repository
        {
            get { return _context.UserOperationDataElements; }
        }
        public void AddSpanOperation(int year, int month, int day, SingleOperation operation)
        {
            _context.UserOperationDataElements.Add(new UserOperationDataElement
                {
                    Day = day,
                    Month = month,
                    Year = year,
                    OperationId = operation.Id,
                    OperationName = operation.OperationName,
                    OperationValue = operation.Value,
                    OperationType = operation.OperationName,
                    UserName = "akorop",
                    UserID = 1
                });
            _context.SaveChanges();
        }

        public void ChangeSpanOperation(int year, int month, int day, int id, SingleOperation operation)
        {
        }

        public void RemoveSpanOperation(int year, int month, int day, int id)
        {
        }
    }
}