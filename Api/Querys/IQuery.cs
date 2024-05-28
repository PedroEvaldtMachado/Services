using Api.Domain.Entities;
using Api.Dtos;

namespace Api.Querys
{
    public interface IQuery<E, D> 
        where E : BaseEntity
        where D : BaseDto
    {
        Task<ICollection<D>?> GetAll();
        Task<D?> GetById(Guid id);
    }
}