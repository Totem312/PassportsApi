using System.Net;
using WebApi.Interfases;

namespace WebApi
{
    public class DownloadFileClass :IDownload
    {
     public void Download()
        {
            var builder = WebApplication.CreateBuilder();
            //string link = @"http://xn--b1ab2a0a.xn--b1aew.xn--p1ai/upload/expired-passports/list_of_expired_passports.csv.bz2";
            //string link = @"https://naked-science.ru/wp-content/uploads/2018/04/field_image_istock-516189065.jpg";
            string link = builder.Configuration["URL"];
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
