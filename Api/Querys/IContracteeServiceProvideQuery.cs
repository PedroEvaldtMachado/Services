using Api.Domain.Entities.Services;
using Api.Dtos.Services;

namespace Api.Querys
{
    public interface IContracteeServiceProvideQuery : IQuery<ContracteeServiceProvide, ContracteeServiceProvideDto>
    {
        Task<IEnumerable<ContracteeServiceProvideDto>> GetByContracteeId(long contracteeId);
        Task<IEnumerable<ContracteeServiceProvideDto>> Search(string value);
    }
}
