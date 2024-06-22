using Api.Domain.Entities.Persons;
using Api.Dtos;
using Api.Dtos.Persons;

namespace Api.Querys
{
    public interface IPersonQuery : IQuery<Person, PersonDto>
    {
        IEnumerable<EnumDto> GetAllPersonDetailTypes();
        Task<IEnumerable<PersonDto>> Search(string value);
    }
}
