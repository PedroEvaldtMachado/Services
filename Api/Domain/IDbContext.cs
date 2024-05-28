using Api.Domain.Entities;
using MongoDB.Driver;

namespace Api.Domain
{
    public interface IDbContext
    {
        IMongoDatabase Database { get; }

        IMongoCollection<T> GetCollection<T>() where T : BaseEntity;
    }
}