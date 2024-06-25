using Api.Domain.Entities;

namespace Api.Repositorys
{
    public interface IRepository<E>
        where E : BaseEntity
    {
        IQueryable<E> Queryable { get; }

        Task<E> InsertAsync(E ent);
        Task<IEnumerable<E>> InsertAsync(IEnumerable<E> ents);

        void Update(E ent);
        void Update(IEnumerable<E> ents);

        void Delete(E ent);
        void Delete(IEnumerable<E> ents);
    }
}
