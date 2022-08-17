using Microsoft.EntityFrameworkCore;
using System.Data;
namespace WebApi
{
  
    public class ApplicationContext : DbContext
    {
        public DbSet<Passport> Passports { get; set; } = null;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   
        }
      
    }
}
