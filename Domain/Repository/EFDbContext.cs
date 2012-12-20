using System.Data.Entity;
using Domain.Blog;
using Domain.User;

namespace Domain.Repository
{
    /// <summary>
    /// Class that implements DbContext for operations repository
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
        /// <summary>
        /// Connections for table <see cref="UserInformation"/>
        /// </summary>
        public DbSet<Article> BlogRepository { get; set; }
    }

  /*  /// <summary>
    /// Class that implements DbContext for user repository
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Connections for table <see cref="UserInformation"/>
        /// </summary>
        public DbSet<UserInformation> UserProfile { get; set; }
    }
    /// <summary>
    /// Class that implements DbContext for Entity Frameworks
    /// </summary>
    public class EFBlogContext : DbContext
    {
        /// <summary>
        /// Connections for table <see cref="UserInformation"/>
        /// </summary>
        public DbSet<Article> BlogRepository { get; set; }
    }*/
}