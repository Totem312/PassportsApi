namespace WebApi.Interfases
{
    public interface IDataFile
    {
        Task WriteFile(List<List<Tuple<uint, uint>>> tuples);
    }
}
