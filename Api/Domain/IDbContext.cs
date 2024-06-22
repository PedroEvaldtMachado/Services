using Api.Domain.Entities;
using System.Linq.Expressions;

namespace Api.Domain
{
    public interface IDbContext
    {
        Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        IQueryable<T> Queryable<T>() where T : BaseEntity;

        Task InsertOneAsync<T>(T ent) where T : BaseEntity;
        Task InsertManyAsync<T>(IEnumerable<T> ents) where T : BaseEntity;

        Task<long> UpdateOneAsync<T>(T ents) where T : BaseEntity;
        Task<long> UpdateManyAsync<T>(IEnumerable<T> ents) where T : BaseEntity;

        Task<long> DeleteOneAsync<T>(T id) where T : BaseEntity;
        Task<long> DeleteManyAsync<T>(IEnumerable<T> ids) where T : BaseEntity;
    }
}