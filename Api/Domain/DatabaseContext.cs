using Api.Domain.Entities;
using Api.Domain.Implementations;
using Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Api.Domain
{
    public abstract class DatabaseContext : DbContext
    {
        protected readonly IOptions<AppSettings> _appSettings;
        private readonly Lazy<IHostEnvironment> _hostEnvironment;

        public DatabaseContext(IOptions<AppSettings> appSettings, Lazy<IHostEnvironment> hostEnvironment)
        {
            _appSettings = appSettings;
            _hostEnvironment = hostEnvironment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging(_hostEnvironment.Value.IsDevelopment());
            optionsBuilder.EnableDetailedErrors(_hostEnvironment.Value.IsDevelopment());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddEntities();
        }

        public virtual IQueryable<E> Queryable<E>(bool asNoTracking = false)
            where E : BaseEntity
        {
            var query = Set<E>();

            if (asNoTracking)
            {
                return query.AsNoTrackingWithIdentityResolution();
            }
            else
            {
                return query;
            }
        }

        public virtual async Task InsertOneAsync<E>(E ent)
            where E : BaseEntity
        {
            await Set<E>().AddAsync(ent);
        }

        public virtual async Task InsertManyAsync<E>(IEnumerable<E> ents)
            where E : BaseEntity
        {
            await Set<E>().AddRangeAsync(ents);
        }

        public virtual void UpdateOneAsync<E>(E ent)
            where E : BaseEntity
        {
            Set<E>().Update(ent);
        }

        public virtual void UpdateManyAsync<E>(IEnumerable<E> ents)
            where E : BaseEntity
        {
            Set<E>().UpdateRange(ents);
        }

        public virtual void DeleteOneAsync<E>(E ent)
            where E : BaseEntity
        {
            Set<E>().Remove(ent);
        }

        public virtual void DeleteManyAsync<E>(IEnumerable<E> ents)
            where E : BaseEntity
        {
            Set<E>().RemoveRange(ents);
        }
    }
}
