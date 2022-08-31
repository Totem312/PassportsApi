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
        IDownload _download;
     
        IReadFile _readFile;


        public OperationController(
                      IServiseRepository repository,
                      IDownload download,
                      IExtract extract,
                      IReadFile readFile
,
                      IFileAddingToDb fileAddingToDb )
        {
            _readFile = readFile;
            _repository = repository;
            _download = download;       
        }

        [HttpGet]
        public List<Passport> GetPasspo()
        {
            return _repository.GetPassports();
        }
        [HttpGet("Ex")]
        public void Download()
        {
            _download.DownloadAsync();
        }
      
        //[HttpGet("Read")]
        //public void Extract()
        //{
        //   _readFile.ReadAllFile();
        //}

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
