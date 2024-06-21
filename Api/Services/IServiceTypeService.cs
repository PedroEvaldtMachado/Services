using Api.Dtos.Services;
using FluentResults;

namespace Api.Services
{
    public interface IServiceTypeService : IService<ServiceTypeDto, NewServiceTypeDto>
    {
    }
}
