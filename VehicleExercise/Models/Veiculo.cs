using Dapper.Contrib.Extensions;

namespace VehicleExercise.Models
{
    [Table("Veiculos")]
    public class Veiculo: ModelBase
    {
        public long Id { get; private set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        public Veiculo(long id, string marca, string modelo)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
        }

        public Veiculo()
        {
            
        }
    }
}
