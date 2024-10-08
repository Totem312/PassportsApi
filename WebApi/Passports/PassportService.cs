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
        public Passport GetPassport(int serial, int number)
        {
            return _db.Passports.FirstOrDefault(x => x.Id == string.Concat(serial,number));

        }
        public List<Passport> GetAllPassports()
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
            _db.Passports.Attach(pass);
            _db.Passports.Remove(pass);
            _db.SaveChanges();
            return null;
        }
        public List<History> GetPassportHistory(int serial, int number)
        {
           return _db.History.Where(x => x.PassportId == string.Concat(serial, number)).ToList();
        }

        public List<History> GetAllChanges(DateTime beginTime, DateTime endTime)
        {
            return _db.History.Where(x => x.DateChangeStatus <= beginTime).ToList();
        }
        public Passport Update(string id, Passport uppassport)
        {
            Passport passport = _db.Passports.Find(id);
            if (uppassport.Series == 0 || uppassport.Number == 0)
            {
                passport.Status = uppassport.Status;
            }
            else
            {
                passport.Number = uppassport.Number;
                passport.Series = uppassport.Series;
                passport.Status = uppassport.Status;
            }
            _db.SaveChanges();
            return passport;
        }
        public async Task ClearTable()
        {
            await _db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Passports]");
        }

        public async Task WriteTextFile()
        {
            await _db.Database.ExecuteSqlRawAsync("call tempPass()");
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



