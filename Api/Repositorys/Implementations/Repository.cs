using Api.Domain;
using Api.Domain.Entities;

namespace Api.Repositorys.Implementations
{
    public class Repository<E> : IRepository<E>
        where E : BaseEntity
    {
        private readonly Lazy<DatabaseContext> Context;

        public IQueryable<E> Queryable { get { return Context.Value.Queryable<E>(false); } }

        public Repository(Lazy<DatabaseContext> databaseContext)
        {
            Context = databaseContext;
        }

        public async Task<E> InsertAsync(E ent)
        {
            await Context.Value.InsertOneAsync(ent);
            return ent;
        }

        public async Task<IEnumerable<E>> InsertAsync(IEnumerable<E> ents)
        {
            await Context.Value.InsertManyAsync(ents);
            return ents;
        }

        public void Update(E ent)
        {
            Context.Value.UpdateOneAsync(ent);
        }

        public void Update(IEnumerable<E> ents)
        {
            Context.Value.UpdateManyAsync(ents);
        }

        public void Delete(E ent)
        {
            Context.Value.DeleteOneAsync<E>(ent);
        }

        public void Delete(IEnumerable<E> ents)
        {
            Context.Value.DeleteManyAsync<E>(ents);
        }
    }
}
