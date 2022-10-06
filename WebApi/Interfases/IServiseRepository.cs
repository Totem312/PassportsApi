using WebApi.Passports;

namespace WebApi.Interfeses
{
    public interface IServiseRepository
    {
        public List<Passport> GetAllPassports();
        public Passport Create(Passport passport);
        public Passport Delete(int id);
        public Passport Update(string id, Passport uppassport);
        public Task MultiThreadingAdd(List<List<(uint, uint)>> rows);
        public Task ClearTable();
        public Task WriteTextFile();
        public Passport GetPassport(int serial, int number);
        public List<History> GetPassportHistory(int serial, int number);
        public List<History> GetAllChanges(DateTime min, DateTime max);
    }
}
