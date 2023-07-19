using Microsoft.AspNetCore.Mvc;
using VehicleExercise.Models;
using VehicleExercise.Repositories;

namespace VehicleExercise.Controllers
{
    [ApiController]
    [Route("veiculos")]
    public class VeiculoController: ControllerBase
    {
        private readonly VeiculoRepository _veiculoRepository;

        public VeiculoController(IConfiguration configuration)
        {
            _veiculoRepository = new VeiculoRepository(configuration);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Veiculo>> Get()
        {
            return Ok(_veiculoRepository.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Veiculo>> Get([FromRoute] long id)
        {
            return Ok(_veiculoRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody]Veiculo entity)
        {
            _veiculoRepository.Add(entity);
            return Created($"veiculos/{entity.Id}", entity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            Veiculo veiculo = _veiculoRepository.Get(id);
            _veiculoRepository.Delete(veiculo);
            return NoContent();
        }

        // XTODO: Finalizar os 2 ultimos Controllers.
    }
}
