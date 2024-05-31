using Api.Dtos;
using Api.Infra.Enums;

namespace Api.Dtos.Contracts
{
    public class ContractDetailDto
    {
        public StakeholderType StakeholderType { get; set; }

        public ContractDetailType ContractDetailType { get; set; }

        public string? Detail { get; set; }
    }
}
