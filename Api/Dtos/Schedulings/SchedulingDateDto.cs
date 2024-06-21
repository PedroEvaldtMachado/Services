using Api.Dtos;

namespace Api.Dtos.Schedulings
{
    public class SchedulingDateDto : BaseDto
    {
        public long ContracteeId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
