namespace Api.Domain.Entities.Services
{
    public class ContracteeServiceProvide : BaseEntity
    {
        public long ServiceTypeId { get; set; }

        public long ContracteeId { get; set; }

        public string? Metric { get; set; }

        public decimal Value { get; set; }

        public string? AdditionalInfo { get; set; }

        public ICollection<ContracteeServiceDetail> ContracteeServiceDetails { get; set; }

        public ContracteeServiceProvide() 
        {
            ContracteeServiceDetails = new List<ContracteeServiceDetail>(); 
        }
    }
}
