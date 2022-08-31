using Quartz;
using WebApi.Interfases;

namespace WebApi.Jobs
{
    internal class LoadDataJob : IJob
    {
        IDownload _download;
        IExtract _extract;
        public LoadDataJob(IDownload download, IExtract extract)
        {
            _download = download;
            _extract = extract;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            string[] filePath = await _download.DownloadAsync();
            string openFile=await _extract.ExtractAsync(filePath);
            //TODO:Необходима реализация добавление файла в бд 

        }
    }
}
