using Api.Domain.Entities;
using Api.Dtos.Persons;

namespace Api.Querys
{
    public interface IPersonQuery : IQuery<Person, PersonDto>
    {
    }
}
