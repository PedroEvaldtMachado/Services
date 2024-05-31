namespace Api.Domain.Entities.Services
{
    public class ContracteeService : BaseEntity
    {
        public Guid ServiceTypeId { get; set; }

        public Guid ContracteeId { get; set; }

        public string? Metric { get; set; }

        public decimal Value { get; set; }

        public string? AdditionalInfo { get; set; }

        public ICollection<ContracteeServiceDetail> ContracteeServiceDetails { get; set; }

        public ContracteeService() 
        {
            ContracteeServiceDetails = new List<ContracteeServiceDetail>(); 
        }
    }
}
