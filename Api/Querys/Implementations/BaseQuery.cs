using Api.Domain.Entities;
using Api.Domain;
using MongoDB.Driver;
using Api.Domain.Implementations;
using Api.Dtos;
using Api.Mappers;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace Api.Querys.Implementations
{
    public class BaseQuery<E, D> : IQuery<E, D> 
        where E : BaseEntity
        where D : BaseDto
    {
        protected readonly IMongoCollection<E> Collection;

        public BaseQuery(BaseQueryParams baseParams)
        {
            Collection = baseParams.ServiceProvider.GetRequiredService<IDbContext>().GetCollection<E>();
        }

        protected async Task<E?> GetEntityById(Guid id)
        {
            var entAsync = await Collection.FindAsync(d => d.Id == id);
            var ent = await entAsync.FirstOrDefaultAsync();

            return ent;
        }

        public async Task<D?> GetById(Guid id)
        {
            var ent = await GetEntityById(id);

            if (ent is null)
            {
                return default;
            }

            return Mapper.Map<E, D?>(ent);
        }

        public async Task<ICollection<D>?> GetAll()
        {
            var list = await Collection.AsQueryable().ToListAsync();

            if (list is null)
            {
                return Array.Empty<D>();
            }

            return list.Select(e => Mapper.Map<E, D>(e)).ToList();
        }
    }

    public class BaseQueryParams
    {
        private readonly Lazy<IServiceProvider> _serviceProvider;

        public IServiceProvider ServiceProvider { get { return _serviceProvider.Value; } }

        public BaseQueryParams(Lazy<IServiceProvider> serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
