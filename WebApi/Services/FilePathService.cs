using System.Security.Policy;
using WebApi.Interfases;

namespace WebApi.Services
{
    public class FilePathService: IFilePathService
    {
       private readonly Settings _settings;
        private string _date;
        public FilePathService(Settings settings)
        {
            _settings = settings;
            _date = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        public string GetArhPath => Path.Combine(GetDirectory(), GetArhFileName());
        public string GetFilePath => Path.Combine(GetDirectory(), GetFileName());
        private string GetDirectory()
        {
            if (String.IsNullOrEmpty(_settings.PathTempFolder))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Temp"));
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                return dirInfo.FullName;
            }
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(_settings.PathTempFolder);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                return dirInfo.FullName;
            }
        }
        private string GetArhFileName()
        {
            string link = _settings.Url;
            string[] halfPath = link.Split('.');
            string extArh = halfPath[halfPath.Length - 1];
            return $"{GetNameFile()}.{extArh}";
        }
        private string GetFileName()
        {
            string link = _settings.Url;
            string[] halfPath = link.Split('.');
            string ext = halfPath[halfPath.Length - 2];
            return $"{GetNameFile()}.{ext}";
        }
        private string GetNameFile()
        {
            string link = _settings.Url;
            string[] halfPath = link.Split('.');

            string extArh = halfPath[halfPath.Length - 3];
            string[] arrayResult = extArh.Split('/');

            string fileName = arrayResult[arrayResult.Length - 1];
            return  fileName+ _date ;
        }
    }
}
