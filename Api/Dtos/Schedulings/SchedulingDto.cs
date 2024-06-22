namespace Api.Dtos.Schedulings
{
    public class SchedulingDto : BaseDto
    {
        public long ContracteeId { get; set; }

        public long? ContractId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string? AdditionalInfo { get; set; }
    }
}
