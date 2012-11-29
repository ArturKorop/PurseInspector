using System.Linq;
using Domain.Abstract;
using Domain.Repository;

namespace Domain.User
{
    /// <summary>
    /// Class that implements interface <see cref="IUserRepository"/>
    /// </summary>
    public class EFUserRepository : IUserRepository
    {
        private readonly EFDbContext _context = new EFDbContext();
        public int GetUserID(string userName)
        {
            return _context.UserProfile.Where(x=>x.UserName == userName).Select(x=>x.UserId).FirstOrDefault();
        }
        public string GetUserName(int userID)
        {
            return userID == 0 ? "test" : _context.UserProfile.Where(x => x.UserId == userID).Select(x => x.UserName).FirstOrDefault();
        }
    }
}