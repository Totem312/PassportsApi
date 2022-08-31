using WebApi;
using WebApi.FileOperation;
using WebApi.Interfases;
using WebApi.Interfeses;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddDbServices();

builder.Services.AddSingleton(_=>builder.Configuration.GetSection("Settings").Get<Settings>());
builder.Services.AddTransient<IExtract, ExtractZipFile>();
builder.Services.AddTransient<IDownload, DownloadFile>();
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