using WebApi.Context;
using WebApi.FileOperation;
using WebApi.Interfases;
using WebApi.Interfeses;
using WebApi.Passports;
using WebApi.Services;
using WebApi.SqlOpration;

var builder = WebApplication.CreateBuilder(args);
builder.AddDbService();
builder.Services.AddScoped<IServiseRepository, PassportService>();
builder.Services.AddSingleton(_ => builder.Configuration.GetSection("Settings").Get<Settings>());
builder.Services.AddTransient<IExtract, ExtractZipFile>();
builder.Services.AddTransient<IDownload, DownloadFile>();
builder.Services.AddScoped<IManagerFile, ManagerFile>();
builder.Services.AddScoped<IFilePathService, FilePathService>();
builder.Services.AddScoped<IDataFile, DataFile>();
builder.Services.AddScoped<IUpdater, Updater>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddServicesQuartz();

builder.AddTaskManger();

var app = builder.Build();

app.UsePostgres();

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
