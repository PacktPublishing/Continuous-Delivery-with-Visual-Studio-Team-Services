using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wonders.Api.Data;
using Wonders.Api.Models;

namespace Wonders.Api.Controllers
{
    public class WondersController : Controller
    {
        private IWondersRepository _repository;
        public WondersController(IWondersRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/[controller]", Name = "GetAll")]
        public async Task<IActionResult> Get()
        {
            var entities = await _repository.All();
            if (entities == null || !entities.Any()) return NoContent();

            return Ok(entities);
        }

        [HttpGet]
        [Route("api/[controller]/{title}", Name = "GetByTitle")]
        public async Task<IActionResult> Get(string title)
        {
            var entity = await _repository.One(title);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Post([FromBody]Wonder wonder)
        {
            if (wonder == null)
            {
                return BadRequest();
            }


            await _repository.New(wonder);

            return CreatedAtRoute("GetByTitle", new
            {
                title = wonder.Title
            }, wonder);
        }
    }
}
