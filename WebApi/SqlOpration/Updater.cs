using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WebApi.Context;
using WebApi.Interfases;

namespace WebApi.SqlOpration
{
    public class Updater : IUpdater
    {
      private readonly ApplicationContext _db;
        IFilePathService _filePath;
        public Updater(ApplicationContext db,IFilePathService filePath)
        {
           _db=db;
            _filePath=filePath;
        }
        public async Task CreateTriggerAsync()
        {
           await _db.Database.ExecuteSqlRawAsync(File.ReadAllText(Path.Combine("SQL", "triggerHistory.sql")));
        }

        public  async Task CreateTempTable()
        {
            await _db.Database.ExecuteSqlRawAsync("call tempPass()");
        }

        public async Task UpdateAsync()
        {
            var Q = $"C:\\Users\\user\\Desktop\\DownloadFile\\1000String.csv";
            for (int i = 0; i < 1; i++)
            {
                string query = $"call load_passports('{Q}')";
                try
                {
                    await _db.Database.ExecuteSqlRawAsync(query);
                }
                catch
                {
                    break;
                }
            }
        }
        public async Task ValidateData()
        {
            await _db.Database.ExecuteSqlRawAsync("call validdata ()");
        }
    }
}
