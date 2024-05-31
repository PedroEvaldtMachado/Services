using Api.Domain.Entities;
using Api.Dtos;
using MongoDB.Driver;

namespace Api.Repositorys
{
    public interface IRepository<E>
        where E : BaseEntity
    {
        IMongoCollection<E> Collection { get; }
    }
}
