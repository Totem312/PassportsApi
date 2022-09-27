using Microsoft.EntityFrameworkCore;
using WebApi.Interfeses;
using WebApi.ParallelManager.Tasks;
using WebApi.ParallelManager;
using WebApi.Context;
using WebApi.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Passports
{
    public class PassportService : IServiseRepository
    {
        ApplicationContext _db;
        TaskManager _taskManager;
        private readonly IManagerFile _manager;
        private readonly IDataFile _dataFile;
        private readonly IFilePathService _filePathService;
        public PassportService(ApplicationContext context,
            TaskManager taskManager,
            IManagerFile manager,
            IDataFile dataFile,
            IFilePathService filePathService)
        {
            _db = context;
            _taskManager = taskManager;
            _manager = manager;
            _dataFile = dataFile;
            _filePathService = filePathService;
        }
        public Passport GetPassport(string id)
        {
            return _db.Passports.FirstOrDefault(x => x.Id == id);

        }
        public List<Passport> GetAllPassports()
        {
            _db.Passports.FromSqlRaw("Exec tempPass");
            //return _db.Passports.ToList();
            return null;
        }

        public Passport Create(Passport passport)
        {
            _db.Passports.Add(passport);
            _db.SaveChanges();
            return passport;
        }
        public Passport Delete(int id)
        {
            Passport pass = new Passport();
            _db.Passports.Attach(pass);
            _db.Passports.Remove(pass);
            _db.SaveChanges();
            return null;
        }

        public Passport Update(int id, Passport uppassport)
        {
            Passport passport = _db.Passports.Find(id);

            passport.Number = uppassport.Number;
            passport.Series = uppassport.Series;

            _db.SaveChanges();
            return passport;
        }
        public async Task ClearTable()
        {
            await _db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Passports]");
        }

        public async Task WriteTextFile()
        {
            var Q = "C:\\Users\\user\\Desktop\\DownloadFile\\data.csv";
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
        public async Task MultiThreadingAdd(List<List<(uint, uint)>> rows)
        {
            _taskManager.For<LoadDataTask>(0, rows.Count, p =>
             {
                 var list = rows[p.CurrentIndex];
                 p.Task = task => task.Execute(async instance =>
                 {
                     await instance._db.Passports.AddRangeAsync(list.Select(x => new Passport { Series = (int)x.Item1, Number = (int)x.Item2 }));
                     instance._db.SaveChanges();
                 });
             });
            await Task.CompletedTask;
        }
    }
}



