using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Configuration;
using System.Net;
using WebApi;
using WebApi.FileOperation;
using WebApi.Interfases;
using WebApi.Interfeses;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
Enum.TryParse<Mode>(builder.Configuration["Mode"],true, out Mode mode);

switch (mode)
{
    case Mode.Ms:
        builder.AddServicesMsSql();
        builder.Services.AddScoped<IServiseRepository, PgPassportService>();
        builder.Services.AddScoped<IFileAddingToDb, AddToDbFilePg>();
        break;
    case Mode.Pg:
        builder.AddServicesPostrges();
        builder.Services.AddScoped<IServiseRepository, PassportService>();
        break;
}

builder.Services.AddSingleton(_=>builder.Configuration.GetSection("Settings").Get<Settings>());
builder.Services.AddTransient<IExtract, ExtractZipFile>();
builder.Services.AddTransient<IDownload, WebApi.FileOperation.DownloadFile>();
builder.Services.AddScoped<IReadFile, ReadFile>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddServicesQuartz();
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
    Ms,
    error
}