using Api.Domain.Entities;
using Api.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Domain.Implementations
{
    public class DbContext : IDbContext
    {
        private readonly IOptions<AppSettings> _appSettings;

        public IMongoDatabase Database
        {
            get
            {
                return GetDatabase(GetClient());
            }
        }

        public DbContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public IMongoCollection<T> GetCollection<T>()
            where T : BaseEntity
        {
            return Database.GetCollection<T>(typeof(T).Name);
        }

        private MongoClient GetClient()
        {
            return new MongoClient(_appSettings.Value.ConnectionDatabase.ConnectionStrings);
        }

        private IMongoDatabase GetDatabase(MongoClient mongoClient)
        {
            return mongoClient.GetDatabase(_appSettings.Value.ConnectionDatabase.DatabaseName);
        }
    }
}
