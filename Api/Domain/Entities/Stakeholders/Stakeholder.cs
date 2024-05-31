using Api.Infra.Enums;

namespace Api.Domain.Entities.Stakeholders
{
    public abstract class Stakeholder : BaseEntity
    {
        public Guid PersonId { get; set; }

        public decimal? Rate { get; set; }
    }
}
