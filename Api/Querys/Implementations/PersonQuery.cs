using Api.Domain.Entities;
using Api.Dtos.Persons;

namespace Api.Querys.Implementations
{
    public class PersonQuery : BaseQuery<Person, PersonDto>, IPersonQuery
    {
        public PersonQuery(BaseQueryParams baseParams) : base(baseParams)
        {
        }
    }
}
