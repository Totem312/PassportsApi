using Quartz;
using System.Net;
using WebApi.Interfases;

namespace WebApi.FileOperation
{
    public class DownloadFile : IDownload
    {
        private readonly Settings _url;

        public DownloadFile(Settings url)
        {
            _url = url;
        }
        public async Task<string[]> DownloadAsync()
        {
            string link = _url.Url;
            string folderPath = _url.PathFolder;
            Random random = new Random();
            string fileName = random.Next().ToString();
            string fileExtension = link.Substring(link.LastIndexOf("."));
            string fullPath = folderPath+ "\\" + fileName ;
            using var client = new WebClient();
            try
            {
                await client.DownloadFileTaskAsync(new Uri(link), fullPath+fileExtension);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
             string[] arrayPath = { fullPath, fileExtension };
            return arrayPath;
        }
    }
}
