using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Stakeholders;

namespace Api.Querys
{
    public interface IContractorQuery : IQuery<Contractor, ContractorDto>
    {
        Task<ContractorDto> GetByPersonId(long personId);
        Task<IEnumerable<ContractorDto>> Search(string value);
    }
}
