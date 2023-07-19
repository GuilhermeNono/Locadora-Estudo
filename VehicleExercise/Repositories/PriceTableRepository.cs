using VehicleExercise.Models;

namespace VehicleExercise.Repositories
{
    public class PriceTableRepository : RepositoryBase<PriceTable, long>
    {
        public PriceTableRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
