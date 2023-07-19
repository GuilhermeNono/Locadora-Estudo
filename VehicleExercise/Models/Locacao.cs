
using Dapper.Contrib.Extensions;

namespace VehicleExercise.Models
{
    [Table("Locacoes")]
    public class Locacao: ModelBase
    {
        public long Id { get; private set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public long KmRetirada { get; set; }
        public long? KmDevolucao { get; set; }
        public bool Paga { get; set; }
        public long IdLocatario { get; set; }
        public long IdPrecoVeiculo { get; set; }

        public Locacao(long id, DateTime inicio, DateTime fim, long kmRetirada, long kmDevolucao, bool paga, long idLocatario, long idPrecoVeiculo)
        {
            Id = id;
            Inicio = inicio;
            Fim = fim;
            KmRetirada = kmRetirada;
            KmDevolucao = kmDevolucao;
            Paga = paga;
            IdLocatario = idLocatario;
            IdPrecoVeiculo = idPrecoVeiculo;
        }

        public Locacao()
        {
            
        }
    }
}
