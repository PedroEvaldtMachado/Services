using Api.Domain.Implementations;
using Api.Domain;
using Api.Domain.Entities;
using Api.Dtos;
using System.Linq;
using Api.Querys;
using Api.Querys.Implementations;
using Api.Services.Implementations;
using Api.Services;
using Api.Repositorys.Implementations;
using Api.Repositorys;
using Api.Domain.Entities.Persons;

namespace Api.Infra
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services) 
        {
            services.AddTransient(typeof(Lazy<>));
            services.AddScoped<IDbContext, DbContext>();

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
            services.AddScoped(typeof(RepositoryParams));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
