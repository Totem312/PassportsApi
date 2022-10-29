using Microsoft.AspNetCore.Mvc;
using WebApi.Interfases;
using WebApi.Interfeses;

using WebApi.Passports;
using WebApi.Migrations;

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
        private readonly IFilePathService _file;

        public OperationController(
                      IServiseRepository repository,
                      IDownload download,
                      IExtract extract,
                      IManagerFile readFile,
                      IFilePathService file
                      )

        {
            _readFile = readFile;
            _repository = repository;
            _download = download;
            _extract = extract;
            _file = file;
        }
        [HttpGet]
        public List<Passport> GetPassports()
        {
            return _repository.GetAllPassports();
        }
        [HttpGet("historyPassport")]
        public List<History> GetPassportHistory(int serial, int number)
        {
            var history = _repository.GetPassportHistory(serial, number);
            if (history == null)
            {
                Response.StatusCode = 400;
            }
            return history;
        }
        [HttpGet("Id")]
        public Passport GetPassport(int serial, int number)
        {
            return _repository.GetPassport(serial, number);
        }
        [HttpGet("Extract")]
        public void Extract()
        {
            _extract.ExtractAsync(_file.GetArhPath);
        }
        [HttpGet("Write")]
        public void Write()
        {
            _repository.WriteTextFile();
        }

        [HttpGet("Download")]
        public void Download()
        {
            _download.DownloadAsync();
        }

        [HttpGet("Read")]
        public async Task ReadAsync()
        {
            var rows = await _readFile.ReadAllFileAsync(_file.GetFilePath);

            await _repository.MultiThreadingAdd(rows);
        }

        [HttpPost("Date")]
        public List<History> GetPassportChanges(DateTimeChangStatus status )
        {
            return _repository.GetAllChanges(status.beginTime, status.endTime);
        }
        [HttpPost()]
        public IActionResult Create(Passport passport)
        {
            _repository.Create(passport);
            return Ok();
        }
        [HttpPost("temp")]
        public IActionResult CreateTemp(Passport passport)
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

        [HttpPut]
        public IActionResult Update(string id, Passport uppassport)
        {
            _repository.Update(id, uppassport);
            return Ok();
        }
    }
}
