using Microsoft.EntityFrameworkCore;
using WebApi.Interfeses;
using WebApi.ParallelManager.Tasks;
using WebApi.ParallelManager;
using WebApi.Context;
using WebApi.Interfases;

namespace WebApi.Passports
{
    public class PassportService : IServiseRepository
    {
        ApplicationContext _db;
        TaskManager _taskManager;
        private readonly IManagerFile _manager;
        private readonly IDataFile _dataFile;
        private readonly IFilePathService _filePathService;
        public PassportService(ApplicationContext context, TaskManager taskManager, IManagerFile manager, IDataFile dataFile, IFilePathService filePathService)
        {
            _db = context;
            _taskManager = taskManager;
            _manager = manager;
            _dataFile = dataFile;
            _filePathService = filePathService; 
        }

        public List<Passport> GetPassports()
        {
            return _db.Passports.ToList();
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
            pass.Id = id;
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
            string wee = (@"C:\Users\user\Desktop\DownloadFile\this.csv");
            //string we = _filePathService.GetTextFilePath;
            //var validList = await _manager.ReadAllFileAsync(@"C:\Users\user\Desktop\DownloadFile\list_of_expired_passports.csv");
            //await _dataFile.WriteFile(validList);
            await _db.Database.ExecuteSqlRawAsync(@$"copy public.passports(serial,number)from'{wee}'WITH(format csv,HEADER TRUE,DELIMITER(','))");
            //await _db.Database.ExecuteSqlRawAsync("DROP TABLE ");
            Console.WriteLine("дошли");

        }
        public async Task MultiThreadingAdd(List<List<Tuple<uint, uint>>> rows)
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



