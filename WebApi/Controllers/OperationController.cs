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
        IExtract _extract;

        public OperationController(
                      IServiseRepository repository,
                      IDownload download,
                      IExtract extract

                                         )
        {

            _repository = repository;
            _download = download;
            _extract = extract;
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
        //[HttpGet("Quarz")]
        //public void DownloadSheduler()
        //{
        //    _scheduler.Start();
        //}
        //[HttpGet("Zip")]
        //public void Extract()
        //{
        //    _extract.Extract();
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
            _repository.Delite(id);
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
