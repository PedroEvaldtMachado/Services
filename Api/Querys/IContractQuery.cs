using Api.Domain.Entities.Contracts;
using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Contracts;
using Api.Dtos.Stakeholders;

namespace Api.Querys
{
    public interface IContractQuery : IQuery<Contract, ContractDto>
    {
        Task<IEnumerable<ContractDto>> GetByContracteeId(long contracteeId);
        Task<IEnumerable<ContractDto>> GetByContractorId(long contractorId);
    }
}
