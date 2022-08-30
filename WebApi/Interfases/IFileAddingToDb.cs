namespace WebApi.Interfases
{
    public interface IFileAddingToDb
    {
        public  Task AddToDb(List<Tuple<uint, uint>> listTuples);
        public Task ClearTable();
    }
}