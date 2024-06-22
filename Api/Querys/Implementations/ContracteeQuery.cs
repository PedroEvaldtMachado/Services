using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Stakeholders;
using Api.Infra;
using Api.Mappers;

namespace Api.Querys.Implementations
{
    public class ContracteeQuery : BaseQuery<Contractee, ContracteeDto>, IContracteeQuery
    {
        private readonly Lazy<IPersonQuery> _personQuery;
        private readonly Lazy<IContracteeServiceProvideQuery> _contracteeServiceProvideQuery;

        public ContracteeQuery(BaseQueryParams baseParams, Lazy<IPersonQuery> personQuery, Lazy<IContracteeServiceProvideQuery> contracteeServiceProvideQuery) : base(baseParams)
        {
            _personQuery = personQuery;
            _contracteeServiceProvideQuery = contracteeServiceProvideQuery;
        }

        public async Task<IEnumerable<ContracteeDto>> Search(string value)
        {
            var byName = await SearchByName(value);
            var byServiceProvided = await SearchByServiceProvided(value);

            return byName.Union(byServiceProvided);
        }

        public async Task<ContracteeDto> GetByPersonId(long personId)
        {
            var ent = await Queryable.FirstOrDefaultTryAsync(c => c.PersonId == personId);
            return Mapper.Map(ent!);
        }

        public async Task<IEnumerable<ContracteeDto>> SearchByName(string value)
        {
            value ??= string.Empty;
            value = value.ToLower();

            var personIds = (await _personQuery.Value.Search(value)).Select(c => c.Id);
            var values = await Queryable.Where(c => personIds.Contains(c.PersonId)).ToListTryAsync();

            return (values ?? new()).Select(c => c.To<ContracteeDto>());
        }

        public async Task<IEnumerable<ContracteeDto>> SearchByServiceProvided(string value)
        {
            value ??= string.Empty;
            value = value.ToLower();

            var contracteesIds = (await _contracteeServiceProvideQuery.Value.Search(value)).Select(c => c.ContracteeId);
            var values = await Queryable.Where(c => contracteesIds.Contains(c.Id)).ToListTryAsync();

            return (values ?? new()).Select(c => c.To<ContracteeDto>());
        }
    }
}
