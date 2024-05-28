using Api.Domain;
using Api.Domain.Entities;
using Api.Dtos.Persons;
using Api.Querys;
using System.Linq;

namespace Api.Services.Implementations
{
    public class PersonService : BaseService<Person, PersonDto>, IPersonService
    {
        public PersonService(BaseServiceParams baseParams) : base(baseParams)
        {
        }
    }
}
