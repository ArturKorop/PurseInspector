using System.Linq;
using Domain.User;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        int GetUserID(string userName);
        string GetUserName(int userID);
    }
}