using Api.Dtos.Stakeholders;
using FluentResults;

namespace Api.Services
{
    public interface IContractorService : IService<ContractorDto, NewContractorDto>
    {
        Task<Result<bool>> DeleteByPerson(long personId);
    }
}
