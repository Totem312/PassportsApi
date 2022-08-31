namespace WebApi.Interfases
{
    public interface IDownload
    {
       public Task<string[]> DownloadAsync();
    }
}