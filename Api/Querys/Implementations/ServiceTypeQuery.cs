using Api.Domain.Entities.Services;
using Api.Dtos;
using Api.Dtos.Services;
using Api.Mappers;
using MongoDB.Driver;
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

            var query = await Collection.FindAsync(c => c.Name!.ToLower().Contains(value) || c.Description!.ToLower().Contains(value));
            var values = await query.ToListAsync();

            return values.Select(c => c.To<ServiceTypeDto>());
        }
    }
}
