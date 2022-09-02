using WebApi.Interfases;
using WebApi.Interfeses;

namespace WebApi
{
    public class Orchestrator
    {
        private readonly IServiseRepository _repository;
        private readonly IDownload _download;
        private readonly IManagerFile _readFile;
        private readonly IExtract _extract;

        public Orchestrator(
                      IServiseRepository repository,
                      IDownload download,
                      IExtract extract,
                      IManagerFile readFile,
                      IFileAddingToDb fileAddingToDb)
        {
            _readFile = readFile;
            _repository = repository;
            _download = download;
            _extract = extract;
        }
    }
}
