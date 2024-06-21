using Api.Domain.Entities.Services;
using Api.Dtos;
using Api.Dtos.Services;
using Api.Mappers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Api.Querys.Implementations
{
    public class ContracteeServiceProvideQuery : BaseQuery<ContracteeServiceProvide, ContracteeServiceProvideDto>, IContracteeServiceProvideQuery
    {
        private readonly Lazy<IServiceTypeQuery> _serviceTypeQuery;

        public ContracteeServiceProvideQuery(BaseQueryParams baseParams, Lazy<IServiceTypeQuery> serviceTypeQuery) : base(baseParams)
        {
            _serviceTypeQuery = serviceTypeQuery;
        }

        public async Task<IEnumerable<ContracteeServiceProvideDto>> Search(string value)
        {
            value ??= string.Empty;
            value = value.ToLower();

            var servicesTypes = (await _serviceTypeQuery.Value.Search(value)).Select(s => s.Id);

            var query = await Collection.FindAsync(c => servicesTypes.Contains(c.ServiceTypeId) || c.AdditionalInfo!.ToLower().Contains(value));
            var values = await query.ToListAsync();

            return values.Select(c => c.To<ContracteeServiceProvideDto>());
        }

        public async Task<IEnumerable<ContracteeServiceProvideDto>> GetByContracteeId(long contracteeId)
        {
            var query = await Collection.FindAsync(c => c.ContracteeId == contracteeId);
            var values = await query.ToListAsync();

            return values.Select(c => c.To<ContracteeServiceProvideDto>());
        }
    }
}
