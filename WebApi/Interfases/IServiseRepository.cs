using WebApi.Passports;

namespace WebApi.Interfeses
{
    public interface IServiseRepository
    {
        public List<Passport> GetAllPassports();
        public Passport Create(Passport passport);
        public Passport Delete(int id);
        public Passport Update(int id, Passport uppassport);
        public Task MultiThreadingAdd(List<List<(uint, uint)>> rows);
        public Task ClearTable();
        public Task WriteTextFile();
        public Passport GetPassport(string id);
    }
}
