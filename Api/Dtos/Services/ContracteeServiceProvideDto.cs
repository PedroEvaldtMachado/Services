using Api.Dtos;

namespace Api.Dtos.Services
{
    public class ContracteeServiceProvideDto : BaseDto
    {
        public long ServiceTypeId { get; set; }

        public long ContracteeId { get; set; }

        public string? Metric { get; set; }

        public decimal Value { get; set; }

        public string? AdditionalInfo { get; set; }

        public ICollection<ContracteeServiceDetailDto> ContracteeServiceDetail { get; set; }

        public ContracteeServiceProvideDto() 
        {
            ContracteeServiceDetail = new List<ContracteeServiceDetailDto>();
        }
    }
}
