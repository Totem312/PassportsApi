namespace WebApi.Interfases
{
    public interface IDownload
    {
        Task<string> DownloadAsync();
    }
}