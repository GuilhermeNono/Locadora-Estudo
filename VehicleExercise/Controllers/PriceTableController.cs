using Microsoft.AspNetCore.Mvc;
using VehicleExercise.Models;
using VehicleExercise.Repositories;

namespace VehicleExercise.Controllers
{
    [ApiController]
    [Route("price-tables")]
    public class PriceTableController: ControllerBase
    {
        private readonly PriceTableRepository _priceTableRepository;

        public PriceTableController(IConfiguration configuration)
        {
            _priceTableRepository = new PriceTableRepository(configuration);
        }

        [HttpGet("{id}")]
        public ActionResult<PriceTable> Get([FromRoute]long id)
        {
            return Ok(_priceTableRepository.Get(id));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PriceTable>> GetAll()
        {
            return Ok(_priceTableRepository.Get());
        }

        [HttpPost]
        public IActionResult Create([FromBody]PriceTable priceTable)
        {
            _priceTableRepository.Add(priceTable);
            return Created($"price-tables/{priceTable.Id}", priceTable);
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute]long id, [FromBody]PriceTable request)
        {
            PriceTable table = _priceTableRepository.Get(id);

            if (request.Final != table.Final)
                table.Final = request.Final;

            if (request.Descrip != table.Descrip)
                table.Descrip = request.Descrip;

            _priceTableRepository.Update(table);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]long id) 
        {
            PriceTable priceTable = _priceTableRepository.Get(id);
            _priceTableRepository.Delete(priceTable);
            return NoContent();
        }
    }
}
