using Api.Domain.Entities.Persons;

namespace Api.Domain.Entities.Stakeholders
{
    public abstract class Stakeholder : BaseEntity
    {
        public long PersonId { get; set; }

        public decimal? Rate { get; set; }

        public virtual Person Person { get; set; }
    }
}
