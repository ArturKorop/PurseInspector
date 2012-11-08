using System.Data.Entity;
using Domain.User;

namespace Domain.Repository
{
    public class EFDbContext : DbContext   
    {
        public DbSet<RepositoryOperation> RepositoryOperations { get; set; }
        public DbSet<UserInformation> UserProfile { get; set; }
    }
}