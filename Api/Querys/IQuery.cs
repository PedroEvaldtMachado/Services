using Api.Domain.Entities;
using Api.Dtos;

namespace Api.Querys
{
    public interface IQuery<E, D>
        where E : BaseEntity
        where D : BaseDto
    {
        IQueryable<E> Queryable { get; }

        Task<ICollection<D>> GetAll();
        Task<D?> GetById(long id);
    }
}