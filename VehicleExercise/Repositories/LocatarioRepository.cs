using VehicleExercise.Models;

namespace VehicleExercise.Repositories
{
    public class LocatarioRepository : RepositoryBase<Locatario, long>
    {
        public LocatarioRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
