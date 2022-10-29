using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Passports;

namespace WebApi.Context
{

    public class ApplicationContext : DbContext
    {
        public DbSet<Passport> Passports { get; set; } = null;
        public DbSet<History> History { get; set; } = null;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Passport>()
        //        .HasKey(c => new { c.Series, c.Number }).HasName("Id");
        //}
    }
}
