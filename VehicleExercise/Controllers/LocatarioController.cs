using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VehicleExercise.Domain;
using VehicleExercise.Models;
using VehicleExercise.Repositories;

namespace VehicleExercise.Controllers
{
    [ApiController]
    [Route("locatarios")]
    public class LocatarioController : ControllerBase
    {
        private readonly IEnumerable<LocacaoDomain> _locacoes;
        private readonly LocacaoRepository _locacaoRepository;
        private readonly LocatarioRepository _locatarioRepository;

        public LocatarioController(IConfiguration configuration)
        {
            _locacoes = GerarBaseDeDados();
            _locacaoRepository = new LocacaoRepository(configuration);
            _locatarioRepository = new LocatarioRepository(configuration);
        }

        private IEnumerable<LocacaoDomain> GerarBaseDeDados()
        {
            List<Locatario> locatarios = new List<Locatario>
            {
                new Locatario(1, "Guilherme Nono"),
                new Locatario(2, "Eliel Silva"),
                new Locatario(3, "Giovanni Fernandes")
            };

            List<Veiculo> veiculos = new List<Veiculo>
            {
                new Veiculo(1, "Ford", "Range"),
                new Veiculo(2, "Volkswagem", "Gol G3")
            };

            List<PriceTable> priceTable = new List<PriceTable>
            {
                new PriceTable(1, "Julho", DateTime.Now, DateTime.Now),
            };

            List<PriceTableVeiculo> priceTableVeiculos = new List<PriceTableVeiculo>
            {
                new PriceTableVeiculo(1, 97.41, 0.90, 200, 1, 1),
                new PriceTableVeiculo(1, 77.41, 0.80, 200, 1, 2)
            };


            var locacoes = new List<LocacaoDomain>();

            for (int i = 0; i < 10; i++)
            {
                long kmRetirada = GerarKmAleatorio(0, 10000);
                long kmDevolucao = GerarKmAleatorio(kmRetirada, 10000);

                var priceTableVeiculoAleatorio = RecuperarObjetoAleatorio<PriceTableVeiculo>(priceTableVeiculos);
                var locatarioAleatorio = RecuperarObjetoAleatorio<Locatario>(locatarios);

                var priceTableVeiculoDomain = new PriceTableVeiculoDomain(RecuperarObjetoAleatorio<PriceTable>(priceTable),
                                                                          RecuperarObjetoAleatorio<Veiculo>(veiculos))
                {
                    Id = priceTableVeiculoAleatorio.Id,
                    PriceDiaria = priceTableVeiculoAleatorio.PriceDiaria,
                    KmLimitPerDay = priceTableVeiculoAleatorio.KmLimitPerDay,
                    PriceKmAdicional = priceTableVeiculoAleatorio.PriceKmAdicional
                };

                var domainResponse = new LocacaoDomain(locatarioAleatorio, priceTableVeiculoDomain);

                domainResponse.Id = i + 1;
                domainResponse.Inicio = GerarDataInicioAleatoria();
                domainResponse.Fim = GerarDataTerminoAleatoria();
                domainResponse.KmRetirada = kmRetirada;
                domainResponse.KmDevolucao = kmDevolucao;
                domainResponse.Paga = GerarBoleanoAleatorio();

                locacoes.Add(domainResponse);
            }

            return locacoes;
        }

        private static DateTime GerarDataInicioAleatoria()
        {
            Random random = new Random();
            int numeroDias = random.Next(1, 30); // Gera um número aleatório entre 1 e 30

            DateTime dataAtual = DateTime.Today;
            DateTime dataInicio = dataAtual.AddDays(-numeroDias);

            return dataInicio;
        }

        private static DateTime GerarDataTerminoAleatoria()
        {
            Random random = new Random();
            int numeroDias = random.Next(1, 30); // Gera um número aleatório entre 1 e 30

            DateTime dataAtual = DateTime.Today;
            DateTime dataTermino = dataAtual.AddDays(numeroDias);

            return dataTermino;
        }

        private static long GerarKmAleatorio(long min, long max)
        {
            Random random = new Random();
            return random.NextInt64(min, max);
        }

        private static bool GerarBoleanoAleatorio()
        {
            Random random = new Random();
            return random.Next(2) == 0;
        }

        private static T RecuperarObjetoAleatorio<T>(List<T> obj)
        {
            var random = new Random();
            int indiceAleatorio = random.Next(obj.Count);
            return obj[indiceAleatorio];
        }

        [HttpGet]
        public ActionResult<IEnumerable<Locatario>> Get()
        {
            return Ok(_locatarioRepository.Get());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Locatario locatario)
        {
            _locatarioRepository.Add(locatario);
            return Created($"locatarios/{locatario.Id}", locatario);
        } 

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] long id, [FromBody] Locatario locatario)
        {
            var locatarioEntity = _locatarioRepository.Get(id);

            if (locatario.Nome != null)
                locatarioEntity.Nome = locatario.Nome;

            _locatarioRepository.Update(locatarioEntity);

            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Locatario>> Get([FromRoute]long id)
        {
            return Ok(_locatarioRepository.Get(id));
        }

        [HttpGet("{id}/nao-pagas")]
        public ActionResult<IEnumerable<LocacaoDomain>> NaoPagas([FromRoute]long id)
        {
            return Ok(_locacaoRepository.NaoPagas(id));
        }

        [HttpGet("{id}/divida")]
        public ActionResult<double> GetDividaTotal([FromRoute] long id)
        {
            IEnumerable<LocacaoDomain> locacoesNaoPagas = _locacaoRepository.NaoPagas(id);

            return Ok(_locacaoRepository.Divida(locacoesNaoPagas));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            Locatario locatario = _locatarioRepository.Get(id);

            _locatarioRepository.Delete(locatario);

            return NoContent();
        }       
    }
}
