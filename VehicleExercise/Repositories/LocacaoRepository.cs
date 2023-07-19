using Dapper;
using System.Text;
using VehicleExercise.Domain;
using VehicleExercise.Models;

namespace VehicleExercise.Repositories
{
    public class LocacaoRepository : RepositoryBase<Locacao, long>
    {
        public LocacaoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public double Divida(IEnumerable<LocacaoDomain> locacoesNaoPagas)
        {
            double totalDivida = 0;
            foreach (var locacao in locacoesNaoPagas)
            {
                totalDivida += locacao.Divida();
            }
            return totalDivida;
        }

        public IEnumerable<LocacaoDomain> NaoPagas(long idLocatario)
        {
            StringBuilder sql = new();
            sql.Append("SELECT * ");
            sql.Append(" FROM Locacoes lc ");
            sql.Append(" INNER JOIN Locatarios lt ON (lt.Id = lc.IdLocatario) ");
            sql.Append(" INNER JOIN PriceTableVeiculos ptv ON (ptv.Id = lc.IdPrecoVeiculo) ");
            sql.Append(" INNER JOIN PriceTables pt ON (pt.Id = ptv.IdPriceTable) ");
            sql.Append(" INNER JOIN Veiculos vc ON (vc.Id = ptv.IdVeiculo) ");
            sql.Append(" WHERE lc.idLocatario = @idLocatario");
            sql.Append(" And lc.Paga = @Paga ");

            using var connection = GetConnection();

            return connection.Query<Locacao, Locatario, PriceTableVeiculo, PriceTable, Veiculo, LocacaoDomain>(
                sql.ToString(), (locacao, locatario, priceTableVeiculo, priceTable, veiculo) =>
            {
                // XTODO: Criar o obejto LocacaoDomain com as informações presentes no parametro Callback
                //return new LocacaoDomain(locatario, new PriceTableVeiculoDomain(priceTable, veiculo));
                var priceTableVeiculoDomain = new PriceTableVeiculoDomain(priceTable, veiculo)
                {
                    Id = priceTableVeiculo.Id,
                    PriceDiaria = priceTableVeiculo.PriceDiaria,
                    KmLimitPerDay = priceTableVeiculo.KmLimitPerDay,
                    PriceKmAdicional = priceTableVeiculo.PriceKmAdicional
                };

                var locacaoDomain = new LocacaoDomain(locatario, priceTableVeiculoDomain)
                {
                    Id = locacao.Id,
                    KmRetirada = locacao.KmRetirada,
                    Fim = locacao.Fim ?? DateTime.MinValue,
                    Inicio = locacao.Inicio,
                    KmDevolucao = locacao.KmDevolucao ?? 0,
                    Paga = locacao.Paga
                };

                return locacaoDomain;
            }, new
            {
                // HINT: Isso é uma classe anônima, e os valores abaixo fazem referencia aos @Params presentes no SQLBuilder
                idLocatario,
                Paga = false
            });

            // XTODO: Separar as responsabilidades pelas classes de dominio

            // TODO: Adicionar novos usuarios ao Banco de dados utilizando o "GerarBaseDeDados()"
        }
    }
}
