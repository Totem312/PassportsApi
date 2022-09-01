namespace WebApi.Interfases
{
    public interface IManagerFile
    {
        public Task <List<List<Tuple<uint, uint>>>> ReadAllFileAsync(string fileName);
    }
}
