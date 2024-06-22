using Api.Domain.Entities;
using Api.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Api.Domain.Implementations
{
    public class MongoDbContext : IDbContext
    {
        private readonly IOptions<AppSettings> _appSettings;

        public IMongoDatabase Database
        {
            get
            {
                return GetDatabase(GetClient());
            }
        }

        public MongoDbContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public IQueryable<E> Queryable<E>() where E : BaseEntity => GetCollection<E>().AsQueryable();

        public async Task<IEnumerable<E>> FindAsync<E>(Expression<Func<E, bool>> expression) where E : BaseEntity
        {
            return await (await GetCollection<E>().FindAsync(expression)).ToListAsync();
        }

        public async Task InsertOneAsync<E>(E ent) where E : BaseEntity => await GetCollection<E>().InsertOneAsync(ent);
        public async Task InsertManyAsync<E>(IEnumerable<E> ents) where E : BaseEntity => await GetCollection<E>().InsertManyAsync(ents);

        public async Task<long> UpdateOneAsync<E>(E ent) where E : BaseEntity
        {
            var result = await GetCollection<E>().UpdateOneAsync(Builders<E>.Filter.Eq(e => e.Id, ent.Id), Builders<E>.Update.Set(e => e, ent));
            return result?.IsModifiedCountAvailable == true ? result.ModifiedCount : 0;
        }

        public async Task<long> UpdateManyAsync<E>(IEnumerable<E> ents) where E : BaseEntity
        {
            long modifiedCount = 0;

            foreach (var ent in ents)
            {
                modifiedCount += await UpdateOneAsync(ent);
            }

            return modifiedCount;
        }

        public async Task<long> DeleteOneAsync<E>(E ent) where E : BaseEntity
        {
            var result = await GetCollection<E>().DeleteOneAsync(e => e.Id == ent.Id);
            return result?.IsAcknowledged == true ? result.DeletedCount : 0;
        }

        public async Task<long> DeleteManyAsync<E>(IEnumerable<E> ents) where E : BaseEntity
        {
            var ids = ents.Select(e => e.Id);
            var result = await GetCollection<E>().DeleteManyAsync(e => ids.Contains(e.Id));

            return result?.IsAcknowledged == true ? result.DeletedCount : 0;
        }

        private IMongoCollection<T> GetCollection<T>() where T : BaseEntity => Database.GetCollection<T>(typeof(T).Name);

        private MongoClient GetClient() => new MongoClient(_appSettings.Value.ConnectionDatabase.ConnectionStrings);

        private IMongoDatabase GetDatabase(MongoClient mongoClient) => mongoClient.GetDatabase(_appSettings.Value.ConnectionDatabase.DatabaseName);
    }
}
