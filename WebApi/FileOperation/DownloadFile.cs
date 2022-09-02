using Quartz;
using System.Net;
using WebApi.Interfases;
using WebApi.Services;

namespace WebApi.FileOperation
{
    public class DownloadFile : IDownload
    {
        private readonly Settings _url;
        IFilePathService _path;

        public DownloadFile(Settings url, IFilePathService path)
        {
            _url = url;
            _path = path;
        }
        public async Task<string> DownloadAsync()
        {
            string link = _url.Url;
            string filArhPath = _path.GetArhPath;
            using var client = new WebClient();
            try
            {
                await client.DownloadFileTaskAsync(new Uri(link), filArhPath);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return filArhPath;
        }
    }
}
