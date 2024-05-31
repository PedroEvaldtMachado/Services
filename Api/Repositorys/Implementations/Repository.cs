using Api.Domain;
using Api.Domain.Entities;
using Api.Dtos;
using Api.Querys.Implementations;
using MongoDB.Driver;

namespace Api.Repositorys.Implementations
{
    public class Repository<E> : IRepository<E>
        where E : BaseEntity
    {
        public IMongoCollection<E> Collection { get; init; }

        public Repository(RepositoryParams baseParams)
        {
            Collection = baseParams.ServiceProvider.GetRequiredService<IDbContext>().GetCollection<E>();
        }
    }

    public class RepositoryParams
    {
        private readonly Lazy<IServiceProvider> _serviceProvider;

        public IServiceProvider ServiceProvider { get { return _serviceProvider.Value; } }

        public RepositoryParams(Lazy<IServiceProvider> serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
