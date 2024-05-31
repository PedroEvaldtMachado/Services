namespace Api.Domain.Entities.Schedulings
{
    public class SchedulingDate : BaseEntity
    {
        public Guid ContracteeId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
