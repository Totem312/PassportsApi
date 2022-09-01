using Microsoft.AspNetCore.Mvc;
using WebApi.Interfases;
using WebApi.Interfeses;
using Quartz;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class OperationController : ControllerBase
    {
        private readonly IServiseRepository _repository;
        private readonly IDownload _download;
        private readonly IManagerFile _readFile;
        private readonly IExtract _extract;

        public OperationController(
                      IServiseRepository repository,
                      IDownload download,
                      IExtract extract,
                      IManagerFile readFile)
                      
        {
            _readFile = readFile;
            _repository = repository;
            _download = download;
            _extract = extract;
        }

        [HttpGet]
        public List<Passport> GetPasspo()
        {
            return _repository.GetPassports();
        }
        [HttpGet("Extract")]
        public void Extract()
        {
            _extract.ExtractAsync(Array.Empty<string>());
        }

        [HttpGet("Download")]
        public void Download()
        {
            _download.DownloadAsync();
        }

        [HttpGet("Read")]
        public async Task Read()
        {
            var rows = await _readFile.ReadAllFileAsync(@"C:\Users\user\Desktop\DownloadFile\PassportList.csv");

            await _repository.MultiThreadingAdd(rows);
        }

        [HttpPost()]
        public IActionResult Create(Passport passport)
        {
            _repository.Create(passport);
            return Ok();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delite(int id)
        {
            _repository.Delete(id);
            return Ok();
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int id, Passport uppassport)
        {
            _repository.Update(id, uppassport);
            return Ok();
        }
    }
}
