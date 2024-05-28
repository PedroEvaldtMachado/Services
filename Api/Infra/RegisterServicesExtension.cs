using Api.Domain.Implementations;
using Api.Domain;
using Api.Domain.Entities;
using Api.Dtos;
using System.Linq;
using Api.Querys;
using Api.Querys.Implementations;
using Api.Services.Implementations;
using Api.Services;

namespace Api.Infra
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services) 
        {
            services.AddTransient(typeof(Lazy<>));
            services.AddScoped<IDbContext, DbContext>();

            services.AddMappers();
            services.AddQuerys();
            services.AddServices();
        }

        private static void AddMappers(this IServiceCollection services)
        {
            //services.AddTransient<IMapper<Person, PersonDto>, PersonMapper>();
        }

        private static void AddQuerys(this IServiceCollection services)
        {
            services.AddScoped(typeof(BaseQueryParams));
            services.AddScoped(typeof(IQuery<,>), typeof(BaseQuery<,>));
            services.AddScoped<IPersonQuery, PersonQuery>();
            services.AddScoped<ICountryQuery, CountryQuery>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(BaseServiceParams));
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICountryService, CountryService>();
        }
    }
}
