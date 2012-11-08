using System.Linq;
using Domain.User;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<UserInformation> Repository();
        int GetUserID(string userName);
    }
}