using Quartz;
using WebApi.Interfases;

namespace WebApi.Jobs
{
    internal class LoadDataJob : IJob
    {
        IDownload _download;
        IExtract _extract;
        IUpdater _updater;
        IDataFile _dataFile;
        IManagerFile _manager;
        public LoadDataJob(IDownload download, IExtract extract, IUpdater updater, IDataFile dataFile, IManagerFile manager)
        {
            _download = download;
            _extract = extract;
            _updater = updater;
            _dataFile = dataFile;
            _manager = manager;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            string filePath = await _download.DownloadAsync();
            string extractedFile = await _extract.ExtractAsync(filePath);
            var readfile = await _manager.ReadAllFileAsync(extractedFile);
            await _dataFile.WriteFileAsync(readfile);
            //await _updater.CreateTriggerAsync();
            //await _updater.CreateTempTable();
            //await _updater.UpdateAsync();
            //await _updater.ValidateData();
        }
    }
}
