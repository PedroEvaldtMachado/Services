using Api.Infra.Enums;

namespace Api.Domain.Entities.Contracts
{
    public class Contract : BaseEntity
    {
        public Guid ContracteeServiceId { get; set; }

        public Guid ContracteeId { get; set; }

        public Guid ContractorId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string? Price { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public string? AdditionalInfo { get; set; }

        public ICollection<ContractDetail> ContractDetails { get; set; }

        public Contract() 
        { 
            ContractDetails = new List<ContractDetail>();
        }
    }
}
