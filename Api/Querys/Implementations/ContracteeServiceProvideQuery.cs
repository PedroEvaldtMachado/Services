using Api.Domain.Entities.Services;
using Api.Dtos.Services;
using Api.Infra;
using Api.Mappers;

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

            var values = await Queryable.Where(c => servicesTypes.Contains(c.ServiceTypeId) || c.AdditionalInfo!.ToLower().Contains(value)).ToListTryAsync();

            return (values ?? new()).Select(c => c.To<ContracteeServiceProvideDto>());
        }

        public async Task<IEnumerable<ContracteeServiceProvideDto>> GetByContracteeId(long contracteeId)
        {
            var values = await Queryable.Where(c => c.ContracteeId == contracteeId).ToListTryAsync();

            return (values ?? new()).Select(c => c.To<ContracteeServiceProvideDto>());
        }
    }
}
