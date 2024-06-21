using Api.Domain.Entities.Countrys;
using Api.Domain.Entities.Services;
using Api.Dtos.Countrys;
using Api.Dtos.Services;

namespace Api.Querys
{
    public interface IServiceTypeQuery : IQuery<ServiceType, ServiceTypeDto>
    {
        Task<IEnumerable<ServiceTypeDto>> Search(string value);
    }
}
