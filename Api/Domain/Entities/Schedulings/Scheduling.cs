namespace Api.Domain.Entities.Schedulings
{
    public class Scheduling : BaseEntity
    {
        public long ContracteeId { get; set; }

        public long? ContractId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string? AdditionalInfo { get; set; }
    }
}
