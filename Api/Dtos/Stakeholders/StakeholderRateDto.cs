using Api.Infra.Enums;

namespace Api.Dtos.Stakeholders
{
    public class StakeholderRateDto : BaseDto
    {
        public Guid ContracteeServiceId { get; set; }

        public Guid ContracteeId { get; set; }

        public Guid ContractorId { get; set; }

        public StakeholderType RateStakeholder { get; set; }

        public decimal Rate { get; set; }

        public string? AdditionalInfo { get; set; }
    }
}
