using Api.Domain;
using Api.Domain.Entities;
using MongoDB.Driver;

namespace Api.Repositorys.Implementations
{
    public class Repository<E> : IRepository<E>
        where E : BaseEntity
    {
        private readonly Lazy<IDbContext> _dbContext;

        public IQueryable<E> Queryable { get { return _dbContext.Value.Queryable<E>(); } }

        public Repository(Lazy<IDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<E?> FindAsync(long id)
        {
            var ents = await _dbContext.Value.FindAsync<E>(e => e.Id == id);
            return ents.FirstOrDefault();
        }

        public async Task<E> InsertAsync(E ent)
        {
            await _dbContext.Value.InsertOneAsync(ent);
            return ent;
        }

        public async Task<IEnumerable<E>> InsertAsync(IEnumerable<E> ents)
        {
            await _dbContext.Value.InsertManyAsync(ents);
            return ents;
        }

        public async Task<long> UpdateAsync(E ent)
        {
            return await _dbContext.Value.UpdateOneAsync(ent);
        }

        public async Task<long> UpdateAsync(IEnumerable<E> ents)
        {
            return await _dbContext.Value.UpdateManyAsync(ents);
        }

        public async Task<long> DeleteAsync(E ent)
        {
            return await _dbContext.Value.DeleteOneAsync<E>(ent);
        }

        public async Task<long> DeleteAsync(IEnumerable<E> ents)
        {
            return await _dbContext.Value.DeleteManyAsync<E>(ents);
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
