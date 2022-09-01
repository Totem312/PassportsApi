using Microsoft.EntityFrameworkCore;
using WebApi.Interfeses;
using WebApi.ParallelManager.Tasks;
using WebApi.ParallelManager;

namespace WebApi
{
    public class PassportService : IServiseRepository
    {
        ApplicationContext _db;
        TaskManager _taskManager;
        public PassportService(ApplicationContext context, TaskManager taskManager)
        {
            _db = context;
            _taskManager = taskManager;
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



