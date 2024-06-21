using Api.Dtos;

namespace Api.Dtos.Services
{
    public class NewContracteeServiceProvideDto
    {
        public long ServiceTypeId { get; set; }

        public long ContracteeId { get; set; }

        public string? Metric { get; set; }

        public decimal Value { get; set; }

        public string? AdditionalInfo { get; set; }

        public ICollection<ContracteeServiceDetailDto> ContracteeServiceDetail { get; set; }

        public NewContracteeServiceProvideDto()
        {
            ContracteeServiceDetail = new List<ContracteeServiceDetailDto>();
        }
    }
}
