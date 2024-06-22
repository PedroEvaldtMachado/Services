using Api.Domain.Entities.Contracts;
using Api.Dtos.Contracts;
using Api.Infra;
using Api.Mappers;

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
            var values = await Queryable.Where(c => c.ContracteeId == contracteeId).ToListTryAsync();
            var listMapped = (values ?? new()).Select(c => Mapper.Map(c));
            return listMapped.ToList();
        }

        public async Task<IEnumerable<ContractDto>> GetByContractorId(long contractorId)
        {
            var values = await Queryable.Where(c => c.ContractorId == contractorId).ToListTryAsync();
            var listMapped = (values ?? new()).Select(c => Mapper.Map(c));
            return listMapped;
        }
    }
}
