using VehicleExercise.Domain.Base;
using VehicleExercise.Models;

namespace VehicleExercise.Domain
{
    public class LocacaoDomain : DomainBase
    {
        public long Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public long KmRetirada { get; set; }
        public int DiasRodados { get => (Fim - Inicio).Days; }
        public long KmPercorrido { get => KmDevolucao - KmRetirada; }
        public double KmMaximoPercorrido { get => DiasRodados * PrecoVeiculo.KmLimitPerDay; }
        public long KmDevolucao { get; set; }
        public double ValorDiariaTotal { get => (DiasRodados * PrecoVeiculo.PriceDiaria); }
        public double ValorKmAdicionalTotal { get => KmAdicional * PrecoVeiculo.PriceKmAdicional; }
        public double KmAdicional { get=> Math.Max(KmPercorrido, KmMaximoPercorrido) - KmMaximoPercorrido; }
        // Caso o valor Percorrido seja maior do que o MaximoPercorrido; A propriedade deverá retornar o KmAdicionalPercorrido
        // Menor - Maior = 0
        // Maior - Menor = 1
        // Caso o valor MaximoPercorrido seja maior do que o Percorrido; A propriedade deverá retornar 0
        public double ValorTotal { get => ValorKmAdicionalTotal + ValorDiariaTotal; }
        public bool Paga { get; set; }
        public Locatario Locatario { get; private set; }
        public PriceTableVeiculoDomain PrecoVeiculo { get; private set; }

        public LocacaoDomain(Locatario locatario, PriceTableVeiculoDomain precoVeiculo)
        {
            Locatario = locatario;
            PrecoVeiculo = precoVeiculo;
        }
    }
}
