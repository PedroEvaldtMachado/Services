using Api.Infra.Enums;

namespace Api.Domain.Entities.Schedulings
{
    public class Scheduling : BaseEntity
    {
        public Guid ContracteeId { get; set; }

        public Guid? ContractId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string? AdditionalInfo { get; set; }
    }
}
