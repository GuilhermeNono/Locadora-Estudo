namespace VehicleExercise.Models
{
    public class ModelBase
    {
        public DateTime DataRegistro { get; private set; }
        public DateTime? DataAtualizacao { get; set; }

        public ModelBase()
        {
            DataRegistro = DateTime.Now;
        }
    }
}
