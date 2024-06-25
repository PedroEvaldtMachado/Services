using Api.Domain;
using Api.Domain.Entities;
using Api.Dtos;
using Api.Infra;
using Api.Mappers;
using MongoDB.Driver;

namespace Api.Querys.Implementations
{
    public class BaseQuery<E, D> : IQuery<E, D>
        where E : BaseEntity
        where D : BaseDto
    {
        private readonly Lazy<DatabaseContext> Context;

        public IQueryable<E> Queryable { get { return Context.Value.Queryable<E>(true); } }

        public BaseQuery(BaseQueryParams queryParams)
        {
            Context = queryParams.DatabaseContext;
        }

        protected async Task<E?> GetEntityById(long id)
        {
            return await Queryable.FirstOrDefaultTryAsync(d => d.Id == id);
        }

        public async Task<D?> GetById(long id)
        {
            var ent = await GetEntityById(id);

            if (ent is null)
            {
                return default;
            }

            return Mapper.Map<E, D?>(ent);
        }

        public async Task<ICollection<D>> GetAll()
        {
            var list = await Queryable.ToListTryAsync();

            if (list is null)
            {
                return Array.Empty<D>();
            }

            return list.Select(e => Mapper.Map<E, D>(e)).ToList();
        }
    }

    public class BaseQueryParams
    {
        public Lazy<DatabaseContext> DatabaseContext { get; init; }

        public BaseQueryParams(Lazy<DatabaseContext> databaseContext)
        {
            DatabaseContext = databaseContext;
        }
    }
}
