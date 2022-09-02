namespace WebApi.Interfases
{
    public interface IExtract
    {
        public Task<string> ExtractAsync(string path);
    }
}
