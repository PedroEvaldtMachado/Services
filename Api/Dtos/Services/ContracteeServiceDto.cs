using Api.Dtos;

namespace Api.Dtos.Services
{
    public class ContracteeServiceDto : BaseDto
    {
        public Guid ServiceTypeId { get; set; }

        public Guid ContracteeId { get; set; }

        public string? Metric { get; set; }

        public decimal Value { get; set; }

        public string? AdditionalInfo { get; set; }
    }
}
