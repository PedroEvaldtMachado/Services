using Api.Dtos.Stakeholders;
using FluentResults;

namespace Api.Services
{
    public interface IContracteeService : IService<ContracteeDto, NewContracteeDto>
    {
        Task<Result<bool>> DeleteByPerson(Guid personId);
    }
}
