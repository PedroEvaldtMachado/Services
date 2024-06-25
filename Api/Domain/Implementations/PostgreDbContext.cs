using Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Api.Domain.Implementations
{
    public class PostgreDatabaseContext : DatabaseContext
    {
        public PostgreDatabaseContext(IOptions<AppSettings> appSettings, Lazy<IHostEnvironment> hostEnvironment) : base(appSettings, hostEnvironment)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql(_appSettings.Value.ConnectionDatabase.ConnectionStrings + $"Database={_appSettings.Value.ConnectionDatabase.DatabaseName};");
        }
    }
}
