using Api.Dtos;
using Api.Infra.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Dtos.Contracts
{
    public class ContractDto : BaseDto
    {
        public Guid ContracteeServiceId { get; set; }

        public Guid ContracteeId { get; set; }

        public Guid ContractorId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string? Price { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public string? AdditionalInfo { get; set; }
    }
}
