using Quartz;
using System.Net;
using WebApi.Interfases;

namespace WebApi.FileOperation
{
    public class DownloadFile :  IDownload
    {
        private readonly Settings _url;
        public DownloadFile(Settings url)
        {
            _url = url;
        }
        public async Task DownloadAsync()
        {
            string link = _url.Url;
            string FolderPath = @"C:\Users\user\Desktop\DownloadFile\";
            string FileName = @"PassportList22";
            string FileExtension = link.Substring(link.LastIndexOf("."));

            using var client = new WebClient();
            try
            {
                await client.DownloadFileTaskAsync(new Uri(link), FolderPath + FileName + FileExtension);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

        }
    }
}
