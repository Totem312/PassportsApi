namespace WebApi.Interfases
{
    public interface IManagerFile
    {
        public Task <List<List<(uint, uint)>>> ReadAllFileAsync(string fileName);
    }
}
