using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;
using VehicleExercise.Models;

namespace VehicleExercise.Repositories
{
    public abstract class RepositoryBase<TEntity, TID> where TEntity : class
    {
        private readonly IConfiguration _configuration;

        protected RepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected IDbConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using var connection = GetConnection();

            connection.Insert(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using var connection = GetConnection();

            connection.Delete(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using var connection = GetConnection();

            if (entity is ModelBase modelBase)
                modelBase.DataAtualizacao = DateTime.Now;

            connection.Update(entity);
        }

        public virtual TEntity Get(TID id)
        {
            using var connection = GetConnection();

            return connection.Get<TEntity>(id);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            using var connection = GetConnection();
            
            return connection.GetAll<TEntity>();
        }
    }
}
