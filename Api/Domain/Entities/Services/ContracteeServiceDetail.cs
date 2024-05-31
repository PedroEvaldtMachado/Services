using Api.Infra.Enums;

namespace Api.Domain.Entities.Services
{
    public class ContracteeServiceDetail
    {
        public ServiceDetailType ServiceDetailType { get; set; }

        public string? Detail { get; set; }
    }
}
