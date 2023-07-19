using VehicleExercise.Models;

namespace VehicleExercise.Repositories
{
    public class PriceTableVeiculoRepository : RepositoryBase<PriceTableVeiculo, long>
    {
        public PriceTableVeiculoRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
