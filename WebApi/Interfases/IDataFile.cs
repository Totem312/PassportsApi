namespace WebApi.Interfases
{
    public interface IDataFile
    {
        Task WriteFileAsync(List<List<(uint series, uint number)>> tuples);
    }
}
