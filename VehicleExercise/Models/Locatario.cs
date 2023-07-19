
using Dapper.Contrib.Extensions;

namespace VehicleExercise.Models
{
    [Table("Locatarios")]
    public class Locatario: ModelBase
    {
        public long Id { get; private set; }
        public string Nome { get; set; }

        public Locatario(long id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Locatario()
        {

        }

        //public KeyValuePair<double, IEnumerable<Locacao>> Divida(List<Locacao> list)
        //{
        //    IEnumerable<Locacao> locacoesNaoPagas = list.Where(l => l.Paga == false && l.KmDevolucao > 0);
            
        //    double totalDivida = 0;
        //    foreach (var locacao in locacoesNaoPagas)
        //    {
        //        TimeSpan diferencaDasDatas = locacao.Inicio - locacao.Fim;
        //        int diferencaEmDias = diferencaDasDatas.Days;

        //        //var priceTable = locacao.;

        //        var kmPercorrido = locacao.KmPercorrido();
        //        return null;
        //    }
        //}
    }
}
