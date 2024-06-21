using Api.Domain.Entities.Contracts;
using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Contracts;
using Api.Dtos.Stakeholders;
using Api.Mappers;
using MongoDB.Driver;

namespace Api.Querys.Implementations
{
    public class ContractQuery : BaseQuery<Contract, ContractDto>, IContractQuery
    {
        private readonly Lazy<IContracteeQuery> _contracteeQuery;
        private readonly Lazy<IContractorQuery> _contractorQuery;

        public ContractQuery(BaseQueryParams baseParams, Lazy<IContracteeQuery> contracteeQuery, Lazy<IContractorQuery> contractorQuery) : base(baseParams)
        {
            _contracteeQuery = contracteeQuery;
            _contractorQuery = contractorQuery;
        }

        public async Task<IEnumerable<ContractDto>> GetByContracteeId(long contracteeId)
        {
            var query = await Collection.FindAsync(c => c.ContracteeId == contracteeId);
            var listMapped = (await query.ToListAsync()).Select(c => Mapper.Map(c));
            return listMapped.ToList();
        }

        public async Task<IEnumerable<ContractDto>> GetByContractorId(long contractorId)
        {
            var query = await Collection.FindAsync(c => c.ContractorId == contractorId);
            var listMapped = (await query.ToListAsync()).Select(c => Mapper.Map(c));
            return listMapped;
        }
    }
}
