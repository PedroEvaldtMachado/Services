using Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Api.Domain.Implementations
{
    public class MongoDatabaseContext : DatabaseContext
    {
        public MongoDatabaseContext(IOptions<AppSettings> appSettings, Lazy<IHostEnvironment> hostEnvironment) : base(appSettings, hostEnvironment)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMongoDB(_appSettings.Value.ConnectionDatabase.ConnectionStrings!, _appSettings.Value.ConnectionDatabase.DatabaseName!);
        }
    }
}
