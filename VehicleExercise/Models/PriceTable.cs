
using Dapper.Contrib.Extensions;

namespace VehicleExercise.Models
{
    [Table("PriceTables")]
    public class PriceTable: ModelBase
    {
        public long Id { get; private set; }
        public string Descrip { get; set; }
        public DateTime Initial { get; set; }
        public DateTime Final { get; set; }

        public PriceTable(long id, string descrip, DateTime initial, DateTime final)
        {
            Id = id;
            Descrip = descrip;
            Initial = initial;
            Final = final;
        }

        public PriceTable() { }
    }
}
