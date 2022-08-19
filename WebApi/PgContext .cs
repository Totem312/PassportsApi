using Microsoft.EntityFrameworkCore;
using System.Data;
namespace WebApi
{
  
    public class PgContext : DbContext
    {
        public DbSet<Passport> Passports { get; set; } = null;

        public PgContext(DbContextOptions<PgContext> options)
            : base(options)
        {
            Database.EnsureCreated();   
        }
      
    }
}
