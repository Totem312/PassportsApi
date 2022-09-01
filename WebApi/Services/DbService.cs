using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    internal static class DbService
    {
        
        public static void AddServicesPostrges(this WebApplicationBuilder builder)
        {
            string pgConnection = builder.Configuration.GetConnectionString("SqlConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(pgConnection));
        }
        public static void AddServicesMsSql(this WebApplicationBuilder builder)
        {
         
            string Sqlconnection = builder.Configuration.GetConnectionString("SqlConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Sqlconnection));
        }
    }
}
