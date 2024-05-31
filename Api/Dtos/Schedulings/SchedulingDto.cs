using Api.Dtos;
using Api.Infra.Enums;

namespace Api.Dtos.Schedulings
{
    public class SchedulingDto : BaseDto
    {
        public Guid ContracteeId { get; set; }

        public Guid? ContractId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string? AdditionalInfo { get; set; }
    }
}
