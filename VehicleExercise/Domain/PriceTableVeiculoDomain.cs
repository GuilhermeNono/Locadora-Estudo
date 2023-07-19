using VehicleExercise.Domain.Base;
using VehicleExercise.Models;

namespace VehicleExercise.Domain
{
    public class PriceTableVeiculoDomain: DomainBase
    {
        public long Id { get; set; }
        public double PriceDiaria { get; set; }
        public double PriceKmAdicional { get; set; }
        public long KmLimitPerDay { get; set; }
        public PriceTable PriceTable { get; private set; }
        public Veiculo Veiculo { get; private set; }

        public PriceTableVeiculoDomain(PriceTable priceTable, Veiculo veiculo)
        {
            PriceTable = priceTable;
            Veiculo = veiculo;
        }
    }
}
