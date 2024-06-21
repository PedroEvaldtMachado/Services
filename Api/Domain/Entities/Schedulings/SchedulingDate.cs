namespace Api.Domain.Entities.Schedulings
{
    public class SchedulingDate : BaseEntity
    {
        public long ContracteeId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
