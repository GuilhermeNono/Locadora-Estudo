
using Dapper.Contrib.Extensions;

namespace VehicleExercise.Models
{
    [Table("PriceTableVeiculos")]
    public class PriceTableVeiculo: ModelBase
    {
        public long Id { get; private set; }
        public double PriceDiaria { get; set; }
        public double PriceKmAdicional { get; set; }
        public long KmLimitPerDay { get; set; }
        public long IdPriceTable { get; set; }
        public long IdVeiculo { get; set; }

        public PriceTableVeiculo(long id, double price, double kmAdicional, long kmLimitPerDay, long idPriceTable, long idVeiculo)
        {
            Id = id;
            PriceDiaria = price;
            PriceKmAdicional = kmAdicional;
            KmLimitPerDay = kmLimitPerDay;
            IdPriceTable = idPriceTable;
            IdVeiculo = idVeiculo;
        }

        public PriceTableVeiculo()
        {
            
        }
    }
}
