using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Stakeholders;
using Api.Infra;
using Api.Mappers;

namespace Api.Querys.Implementations
{
    public class ContractorQuery : BaseQuery<Contractor, ContractorDto>, IContractorQuery
    {
        private readonly Lazy<IPersonQuery> _personQuery;

        public ContractorQuery(BaseQueryParams baseParams, Lazy<IPersonQuery> personQuery) : base(baseParams)
        {
            _personQuery = personQuery;
        }

        public async Task<IEnumerable<ContractorDto>> Search(string value)
        {
            value ??= string.Empty;
            value = value.ToLower();

            var personIds = (await _personQuery.Value.Search(value)).Select(c => c.Id);
            var values = await Queryable.Where(c => personIds.Contains(c.PersonId)).ToListTryAsync();

            return (values ?? new()).Select(c => c.To<ContractorDto>());
        }

        public async Task<ContractorDto?> GetByPersonId(long personId)
        {
            var value = await Queryable.Where(c => c.PersonId == personId).FirstOrDefaultTryAsync();

            return value is not null ? Mapper.Map(value) : default;
        }
    }
}
