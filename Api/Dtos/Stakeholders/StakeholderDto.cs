using Api.Dtos;
using Api.Infra.Enums;

namespace Api.Dtos.Stakeholders
{
    public abstract class StakeholderDto : BaseDto
    {
        public Guid PersonId { get; set; }

        public decimal? Rate { get; set; }
    }
}
