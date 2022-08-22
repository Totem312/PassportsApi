using System.Net;
using WebApi.Interfases;

namespace WebApi
{
    public class DownloadFileClass : Settings, IDownload
    {
        private readonly Settings _url;
        public DownloadFileClass(Settings url)
        {
            _url=url;
        }
        public void Download()
        {       
            string link = _url.Url;
            string FolderPath = @"C:\Users\user\Desktop\DownloadFile\";
            string FileName = @"PassportList22";
            string FileExtension = link.Substring(link.LastIndexOf("."));

            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(new Uri(link), FolderPath + FileName + FileExtension);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
    }
}
