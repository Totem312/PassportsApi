using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    internal static class DbService
    {
        public static void AddDbService(this WebApplicationBuilder builder)
        {
            Enum.TryParse(builder.Configuration["Mode"], true, out ContextMode.Mode mode);
            switch (mode)
            {
                case ContextMode.Mode.Ms:
                    {
                        string connection = builder.Configuration.GetConnectionString("SqlConnection");
                        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
                    }
                    break;
                case ContextMode.Mode.Pg:
                    {
                        string connection = builder.Configuration.GetConnectionString("PgConnection");
                        builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
                    }
                    break;
            }
        }
    }
}
