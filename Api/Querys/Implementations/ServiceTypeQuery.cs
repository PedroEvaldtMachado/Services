using Api.Domain.Entities.Services;
using Api.Dtos.Services;
using Api.Infra;
using Api.Mappers;
using MongoDB.Driver.Linq;

namespace Api.Querys.Implementations
{
    public class ServiceTypeQuery : BaseQuery<ServiceType, ServiceTypeDto>, IServiceTypeQuery
    {
        public ServiceTypeQuery(BaseQueryParams baseParams) : base(baseParams)
        {
        }

        public async Task<IEnumerable<ServiceTypeDto>> Search(string value)
        {
            value ??= string.Empty;
            value = value.ToLower();

            var values = await Queryable.Where(c => c.Name!.ToLower().Contains(value) || c.Description!.ToLower().Contains(value)).ToListTryAsync();

            return (values ?? new()).Select(c => c.To<ServiceTypeDto>());
        }
    }
}
