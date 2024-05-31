using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Stakeholders;
using Api.Mappers;
using MongoDB.Driver;

namespace Api.Querys.Implementations
{
    public class ContracteeQuery : BaseQuery<Contractee, ContracteeDto>, IContracteeQuery
    {
        private readonly Lazy<IPersonQuery> _personQuery;

        public ContracteeQuery(BaseQueryParams baseParams, Lazy<IPersonQuery> personQuery) : base(baseParams)
        {
            _personQuery = personQuery;
        }

        public async Task<IEnumerable<ContracteeDto>> Search(string value)
        {
            value ??= string.Empty;
            value = value.ToLower();

            var personIds = (await _personQuery.Value.Search(value)).Select(c => c.Id);

            var query = await Collection.FindAsync(c => personIds.Contains(c.PersonId));
            var values = await query.ToListAsync();

            return values.Select(c => c.To<ContracteeDto>());
        }

        public async Task<ContracteeDto> GetByPersonId(Guid personId)
        {
            var query = await Collection.FindAsync(c => c.PersonId == personId);
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
    }
}
