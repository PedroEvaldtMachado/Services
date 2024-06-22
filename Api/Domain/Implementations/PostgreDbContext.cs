using Api.Domain.Entities;
using Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Api.Domain.Implementations
{
    public class PostgreDbContext : DbContext, IDbContext
    {
        private readonly IOptions<AppSettings> _appSettings;

        public PostgreDbContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_appSettings.Value.ConnectionDatabase.ConnectionStrings);
        }

        public IQueryable<E> Queryable<E>() where E : BaseEntity => Set<E>();

        public async Task<IEnumerable<E>> FindAsync<E>(Expression<Func<E, bool>> expression) where E : BaseEntity
        {
            return await Set<E>().Where(expression).ToListAsync();
        }

        public async Task InsertOneAsync<E>(E ent) where E : BaseEntity
        {
            await Set<E>().AddAsync(ent);
            await SaveChangesAsync();
        }

        public async Task InsertManyAsync<E>(IEnumerable<E> ents) where E : BaseEntity
        {
            await Set<E>().AddRangeAsync(ents);
            await SaveChangesAsync();
        }

        public async Task<long> UpdateOneAsync<E>(E ent) where E : BaseEntity
        {
            Set<E>().Update(ent);
            return await SaveChangesAsync();
        }

        public async Task<long> UpdateManyAsync<E>(IEnumerable<E> ents) where E : BaseEntity
        {
            Set<E>().UpdateRange(ents);
            return await SaveChangesAsync();
        }

        public async Task<long> DeleteOneAsync<E>(E ent) where E : BaseEntity
        {
            Set<E>().Remove(ent);
            return await SaveChangesAsync();
        }

        public async Task<long> DeleteManyAsync<E>(IEnumerable<E> ents) where E : BaseEntity
        {
            Set<E>().RemoveRange(ents);
            return await SaveChangesAsync();
        }
    }
}
