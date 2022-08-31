using WebApi.Interfases;
using Microsoft.EntityFrameworkCore;

namespace WebApi.FileOperation
{
    public class AddToDbFilePg:IFileAddingToDb
    {
        ApplicationContext _pg;
        public AddToDbFilePg(ApplicationContext pg)
        {
            _pg = pg;
        }

        public async Task AddToDb(List<Tuple<uint, uint>> listTuples)
        {
          await _pg.Passports.AddRangeAsync(listTuples.Select(x=>new Passport { Series=(int)x.Item1,Number=(int)x.Item2} ));
           _pg.SaveChanges();
        }
        public async Task ClearTable ()
        {
            await _pg.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Passports]");
        }
    }
}
