using Api.Infra.Enums;

namespace Api.Domain.Entities.Stakeholders
{
    public abstract class Stakeholder : BaseEntity
    {
        public long PersonId { get; set; }

        public decimal? Rate { get; set; }
    }
}
