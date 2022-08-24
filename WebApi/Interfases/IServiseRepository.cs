namespace WebApi.Interfeses
{
    public interface IServiseRepository
    {
        public List<Passport> GetPassports();
        public Passport Create(Passport passport);
        public Passport Delite(int id);
        public Passport Update(int id, Passport uppassport);

    }
}
