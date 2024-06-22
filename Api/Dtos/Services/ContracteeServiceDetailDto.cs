using Api.Infra.Enums;

namespace Api.Dtos.Services
{
    public class ContracteeServiceDetailDto
    {
        public ServiceDetailType ServiceDetailType { get; set; }

        public string? Detail { get; set; }
    }
}
