using Api.Domain;
using Api.Domain.Implementations;
using Api.Dtos;
using Api.Infra.Enums;
using Api.Querys;
using Api.Querys.Implementations;
using Api.Repositorys;
using Api.Repositorys.Implementations;
using Api.Services;
using Api.Services.Implementations;

namespace Api.Infra
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddTransient(typeof(Lazy<>));
            services.AddDatabase(appSettings);

            services.AddQuerys();
            services.AddRepositories();
            services.AddServices();
        }

        private static void AddQuerys(this IServiceCollection services)
        {
            services.AddScoped(typeof(BaseQueryParams));
            services.AddScoped(typeof(IQuery<,>), typeof(BaseQuery<,>));
            services.AddScoped<IPersonQuery, PersonQuery>();
            services.AddScoped<ICountryQuery, CountryQuery>();
            services.AddScoped<IContracteeQuery, ContracteeQuery>();
            services.AddScoped<IContracteeServiceProvideQuery, ContracteeServiceProvideQuery>();
            services.AddScoped<IContractorQuery, ContractorQuery>();
            services.AddScoped<IContractQuery, ContractQuery>();
            services.AddScoped<IServiceTypeQuery, ServiceTypeQuery>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<BaseServiceParams>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountryValidation, CountryValidation>();
            services.AddScoped<IContracteeService, ContracteeService>();
            services.AddScoped<IContracteeServiceProvideService, ContracteeServiceProvideService>();
            services.AddScoped<IContractorService, ContractorService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void AddDatabase(this IServiceCollection services, AppSettings appSettings)
        {
            switch (appSettings.ConnectionDatabase.DataBaseType)
            {
                case DataBaseType.MongoDb:
                    services.AddDbContext<DatabaseContext, MongoDatabaseContext>();
                    break;
                case DataBaseType.PostgreSql:
                    services.AddDbContext<DatabaseContext, PostgreDatabaseContext>();
                    break;
                default:
                    break;
            }
        }
    }
}
