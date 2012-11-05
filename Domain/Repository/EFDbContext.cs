using System.Data.Entity;
using System.Linq;
using Domain.Abstract;
using Domain.Purse;

namespace Domain.Repository
{
    public class EFDbContext : DbContext   
    {
        public DbSet<UserOperationDataElement> UserOperationDataElements { get; set; }
    }
}