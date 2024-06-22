using Api.Domain.Entities;

namespace Api.Repositorys
{
    public interface IRepository<E>
        where E : BaseEntity
    {
        IQueryable<E> Queryable { get; }
        Task<E?> FindAsync(long id);

        Task<E> InsertAsync(E ent);
        Task<IEnumerable<E>> InsertAsync(IEnumerable<E> ents);

        Task<long> UpdateAsync(E ent);
        Task<long> UpdateAsync(IEnumerable<E> ents);

        Task<long> DeleteAsync(E ent);
        Task<long> DeleteAsync(IEnumerable<E> ents);
    }
}
