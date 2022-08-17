using Microsoft.AspNetCore.Mvc;
using WebApi.Interfeses;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class OperationController : ControllerBase
    {
       
        private readonly IServiseRepository _repository;
        public OperationController(IServiseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public List<Passport> GetPasspo()
        {     
            
            return _repository.GetPassports();
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
            _repository.Delite(id);
            return Ok();
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int id,Passport uppassport)
        {                   
           _repository.Update(id, uppassport);
                 return Ok();
        }
    }
}
