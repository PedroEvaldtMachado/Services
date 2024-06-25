using Api.Infra.Enums;

namespace Api.Domain.Entities.Services
{
    public class ContracteeServiceDetail : BaseEntity
    {
        public long ContracteeServiceProvideId { get; set; }

        public ServiceDetailType ServiceDetailType { get; set; }

        public string? Detail { get; set; }
    }
}
