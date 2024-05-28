using Api.Domain;
using Api.Domain.Entities;
using Api.Dtos;
using Api.Querys.Implementations;
using MongoDB.Driver;

namespace Api.Services.Implementations
{
    public abstract class BaseService<E, D>
        where E : BaseEntity
        where D : BaseDto
    {
        protected readonly IMongoCollection<E> Collection;

        public BaseService(BaseServiceParams baseParams)
        {
            Collection = baseParams.ServiceProvider.GetRequiredService<IDbContext>().GetCollection<E>();
        }
    }

    public class BaseServiceParams
    {
        private readonly Lazy<IServiceProvider> _serviceProvider;

        public IServiceProvider ServiceProvider { get { return _serviceProvider.Value; } }

        public BaseServiceParams(Lazy<IServiceProvider> serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
