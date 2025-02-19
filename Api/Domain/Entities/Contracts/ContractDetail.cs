﻿using Api.Infra.Enums;

namespace Api.Domain.Entities.Contracts
{
    public class ContractDetail : BaseEntity
    {
        public StakeholderType StakeholderType { get; set; }

        public ContractDetailType ContractDetailType { get; set; }

        public string? Detail { get; set; }
    }
}
