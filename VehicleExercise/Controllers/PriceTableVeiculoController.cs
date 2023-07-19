using Microsoft.AspNetCore.Mvc;
using VehicleExercise.Models;
using VehicleExercise.Repositories;

namespace VehicleExercise.Controllers
{
    [ApiController]
    [Route("price-table-veiculos")]
    public class PriceTableVeiculoController: ControllerBase
    {
        private readonly PriceTableVeiculoRepository _priceTableVeiculoRepository;

        public PriceTableVeiculoController(IConfiguration configuration)
        {
            _priceTableVeiculoRepository = new PriceTableVeiculoRepository(configuration);
        }

        [HttpGet("{id}")]
        public ActionResult<PriceTableVeiculo> Get([FromRoute] long id)
        {
            return Ok(_priceTableVeiculoRepository.Get(id));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PriceTableVeiculo>> GetAll()
        {
            return Ok(_priceTableVeiculoRepository.Get());
        }

        [HttpPost]
        public IActionResult Create([FromBody] PriceTableVeiculo priceTableVeiculo)
        {
            _priceTableVeiculoRepository.Add(priceTableVeiculo);
            return Created($"price-tables/{priceTableVeiculo.Id}", priceTableVeiculo);
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute]long id, [FromBody] PriceTableVeiculo request)
        {
            PriceTableVeiculo tableVeiculo = _priceTableVeiculoRepository.Get( id );

            if( request.PriceDiaria != tableVeiculo.PriceDiaria ) 
                tableVeiculo.PriceDiaria = request.PriceDiaria;

            if (request.PriceKmAdicional != tableVeiculo.PriceKmAdicional)
                tableVeiculo.PriceKmAdicional = request.PriceKmAdicional;

            if (request.KmLimitPerDay != tableVeiculo.KmLimitPerDay)
                tableVeiculo.KmLimitPerDay = request.KmLimitPerDay;

            _priceTableVeiculoRepository.Update( tableVeiculo );
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            PriceTableVeiculo priceTableVeiculo = _priceTableVeiculoRepository.Get(id);
            _priceTableVeiculoRepository.Delete(priceTableVeiculo);
            return NoContent();
        }
    }
}
