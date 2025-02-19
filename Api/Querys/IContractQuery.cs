﻿using Api.Domain.Entities.Contracts;
using Api.Dtos.Contracts;

namespace Api.Querys
{
    public interface IContractQuery : IQuery<Contract, ContractDto>
    {
        Task<IEnumerable<ContractDto>> GetByContracteeId(long contracteeId);
        Task<IEnumerable<ContractDto>> GetByContractorId(long contractorId);
    }
}
