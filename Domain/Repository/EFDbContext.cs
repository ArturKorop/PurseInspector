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

       // public DbSet<Test> Tests { get; set; } 
    }
    /// <summary>
    /// Test class
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ID { get;set ;}
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }
}