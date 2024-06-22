using Api.Dtos.Contracts;
using FluentResults;

namespace Api.Services
{
    public interface IContractService
    {
        public Task<Result<ContractDto>> Request(NewContractDto newDto);

        public Task<Result<ContractDto>> Approve(ContractDto dto);

        public Task<Result<ContractDto>> ApproveWithModifications(ContractDto dto);

        public Task<Result<ContractDto>> Done(ContractDto dto);

        public Task<Result<ContractDto>> Canceled(ContractDto dto);

        public Task<Result<ContractDto>> Reject(ContractDto dto);

        public Task<Result<ContractDto>> Inactive(ContractDto dto);
    }
}
