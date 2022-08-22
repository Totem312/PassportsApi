using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Net;
using WebApi;
using WebApi.Interfases;
using WebApi.Interfeses;

var builder = WebApplication.CreateBuilder(args);
Enum.TryParse<Mode>(builder.Configuration["Mode"],true, out Mode mode);

switch (mode)
{
    case Mode.Pg:
        string Sqlconnection = builder.Configuration.GetConnectionString("SqlConnection");
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Sqlconnection));
        builder.Services.AddScoped<IServiseRepository, PassportService>();
        break;
    case Mode.Sql:
        string Pgconnection = builder.Configuration.GetConnectionString("PgConnection");
        builder.Services.AddDbContext<PgContext>(options => options.UseNpgsql(Pgconnection));
        builder.Services.AddScoped<IServiseRepository, PgPassportService>();
        break;
}
builder.Services.AddSingleton(_=>builder.Configuration.GetSection("Settings").Get<Settings>());
builder.Services.AddTransient<IExtract, ExtractZipFileClass>();
builder.Services.AddTransient<IDownload, DownloadFileClass>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

 enum Mode
{
    Pg,
    Sql,
    error
}