using ICBF.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ICBF.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=.;database=ICBF;Trusted_Connection=True;MultipleActiveResultSets=True;");
            }
        }
    }
}