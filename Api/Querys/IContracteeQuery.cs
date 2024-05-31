using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Stakeholders;

namespace Api.Querys
{
    public interface IContracteeQuery : IQuery<Contractee, ContracteeDto>
    {
        Task<ContracteeDto> GetByPersonId(Guid personId);
        Task<IEnumerable<ContracteeDto>> Search(string value);
    }
}
