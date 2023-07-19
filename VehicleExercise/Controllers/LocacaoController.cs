using Microsoft.AspNetCore.Mvc;
using VehicleExercise.Models;
using VehicleExercise.Repositories;

namespace VehicleExercise.Controllers
{
    [ApiController]
    [Route("locacoes")]
    public class LocacaoController: ControllerBase
    {
        private readonly LocacaoRepository _locacaoRepository;

        public LocacaoController(IConfiguration configuration)
        {
            _locacaoRepository = new LocacaoRepository(configuration);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Locacao>> Get()
        {
            return Ok(_locacaoRepository.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<Locacao> Get([FromRoute]long id)
        {
            return Ok(_locacaoRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody]Locacao locacao)
        {
            _locacaoRepository.Add(locacao);
            return Created($"locacoes/{locacao.Id}", locacao);
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] long id, [FromBody] Locacao request)
        {
            Locacao locacao = _locacaoRepository.Get(id);

            if(request.Paga != request.Paga)
                locacao.Paga = request.Paga;

            if(request.Fim != null) 
                locacao.Fim = request.Fim;

            if(request.KmDevolucao != null)
                locacao.KmDevolucao = request.KmDevolucao;

            _locacaoRepository.Update(locacao);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            Locacao locacao = _locacaoRepository.Get(id);

            _locacaoRepository.Delete(locacao);
            return NoContent();
        }
    }
}
