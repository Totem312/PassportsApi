using Microsoft.EntityFrameworkCore;
using WebApi.Interfeses;

namespace WebApi.Services
{
    internal static class DbService
    {

        public static void AddDbService(this WebApplicationBuilder builder)
        {
            Enum.TryParse(builder.Configuration["Mode"], true, out Mode mode);
            switch (mode)
            {
                case Mode.Ms:
                    {
                        string connection = builder.Configuration.GetConnectionString("SqlConnection");
                        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
                    }
                    break;
                case Mode.Pg:
                    {
                        string connection = builder.Configuration.GetConnectionString("PgConnection");
                        builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
                    }
                    break;
            }

        }
    }
}
