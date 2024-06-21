using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Stakeholders;

namespace Api.Querys
{
    public interface IContracteeQuery : IQuery<Contractee, ContracteeDto>
    {
        Task<ContracteeDto> GetByPersonId(long personId);
        Task<IEnumerable<ContracteeDto>> Search(string value);
        Task<IEnumerable<ContracteeDto>> SearchByName(string value);
        Task<IEnumerable<ContracteeDto>> SearchByServiceProvided(string value);
    }
}
