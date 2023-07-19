using VehicleExercise.Models;

namespace VehicleExercise.Repositories
{
    public class VeiculoRepository : RepositoryBase<Veiculo, long>
    {
        public VeiculoRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
