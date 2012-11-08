using System.Linq;
using Domain.Abstract;
using Domain.Repository;

namespace Domain.User
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<UserInformation> Repository()
        {
            return _context.UserProfile;
        }

        public int GetUserID(string userName)
        {
            return _context.UserProfile.Where(x=>x.UserName == userName).Select(x=>x.UserId).FirstOrDefault();
        }
    }
}