using Api.Infra.Enums;

namespace Api.Domain.Entities.Stakeholders
{
    public class StakeholderRate : BaseEntity
    {
        public Guid ContracteeServiceId { get; set; }

        public Guid ContracteeId { get; set; }

        public Guid ContractorId { get; set; }

        public StakeholderType RateStakeholder { get; set; }

        public decimal Rate { get; set; }

        public string? AdditionalInfo { get; set; }
    }
}
