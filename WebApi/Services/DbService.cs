using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    internal static class DbService
    {
        
        public static void AddServicesPostrges(this WebApplicationBuilder builder)
        {
            string Sqlconnection = builder.Configuration.GetConnectionString("SqlConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Sqlconnection));
        }
        public static void AddServicesMsSql(this WebApplicationBuilder builder)
        {
            string Pgconnection = builder.Configuration.GetConnectionString("PgConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(Pgconnection));
        }
    }
}
