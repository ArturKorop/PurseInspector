using System.Data.Entity;
using System.Linq;

namespace Domain.Repository
{
    public class EFTestContext : DbContext
    {
        public DbSet<Temp> Temps { get; set; }
    }
    public class Temp
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TempsRepository
    {
        private EFTestContext _context = new EFTestContext();
        public IQueryable<Temp> Repository
        {
            get { return _context.Temps; }
        } 
        public void AddTemp(string name)
        {
            _context.Temps.Add(new Temp {Name = "name"});
            _context.SaveChanges();
        }
    }
}