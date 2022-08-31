using Microsoft.EntityFrameworkCore;
using WebApi.FileOperation;
using WebApi.Interfases;
using WebApi.Interfeses;

namespace WebApi.Services
{
    internal static class DbService
    {

        private static void AddServicesPostrges(this WebApplicationBuilder builder)
        {
            string Sqlconnection = builder.Configuration.GetConnectionString("SqlConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Sqlconnection));
        }
        private static void AddServicesMsSql(this WebApplicationBuilder builder)
        {
            string Pgconnection = builder.Configuration.GetConnectionString("PgConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(Pgconnection));
        }
        public static void AddDbServices(this WebApplicationBuilder builder)
        {
            Enum.TryParse<Mode>(builder.Configuration["Mode"], true, out Mode mode);
            switch (mode)
            {
                case Mode.Ms:
                    builder.AddServicesPostrges();
                    builder.Services.AddScoped<IServiseRepository, PgPassportService>();
                    builder.Services.AddScoped<IFileAddingToDb, AddToDbFilePg>();
                    break;
                case Mode.Pg:
                    builder.AddServicesMsSql();
                    builder.Services.AddScoped<IServiseRepository, PassportService>();
                    break;
            }
        }
    }
}
