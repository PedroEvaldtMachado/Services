using Api.Domain.Entities.Schedulings;

namespace Api.Domain.Entities.Stakeholders
{
    public class Contractee : Stakeholder
    {
        public int WorksCount { get; set; }

        public ICollection<Scheduling> Schedulings { get; set; }

        public ICollection<SchedulingDate> SchedulingDates { get; set; }

        public Contractee()
        {
            Schedulings = new List<Scheduling>();
            SchedulingDates = new List<SchedulingDate>();
        }
    }
}
