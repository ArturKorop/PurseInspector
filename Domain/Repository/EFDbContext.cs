using System.Data.Entity;
using Domain.User;

namespace Domain.Repository
{
    /// <summary>
    /// Class that implements DbContext for Entity Frameworks
    /// </summary>
    public class EFDbContext : DbContext   
    {
        /// <summary>
        /// Connections for table <see cref="RepositoryOperation"/>
        /// </summary>
        public DbSet<RepositoryOperation> RepositoryOperations { get; set; }
        /// <summary>
        /// Connections for table <see cref="UserInformation"/>
        /// </summary>
        public DbSet<UserInformation> UserProfile { get; set; }
    }
}