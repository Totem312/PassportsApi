using Microsoft.EntityFrameworkCore;
using WebApi.Passports;

namespace WebApi.Context
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
