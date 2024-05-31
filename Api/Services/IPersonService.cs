using Api.Dtos.Persons;

namespace Api.Services
{
    public interface IPersonService : IService<PersonDto, NewPersonDto>
    {
    }
}
