using Api.Domain.Entities.Persons;
using Api.Dtos;
using Api.Dtos.Persons;
using Api.Infra;
using Api.Infra.Enums;
using Api.Mappers;

namespace Api.Querys.Implementations
{
    public class PersonQuery : BaseQuery<Person, PersonDto>, IPersonQuery
    {
        public PersonQuery(BaseQueryParams baseParams) : base(baseParams)
        {
        }

        public IEnumerable<EnumDto> GetAllPersonDetailTypes()
        {
            var values = Enum.GetValues<PersonDetailType>()
                .Select(e => new EnumDto { Id = (int)e, Name = e.ToString() });

            return values;
        }

        public async Task<IEnumerable<PersonDto>> Search(string value)
        {
            value ??= string.Empty;
            value = value.ToLower();

            var values = await Queryable.Where(c => c.Name!.ToLower().Contains(value) || c.Username!.ToLower().Contains(value)).ToListTryAsync();

            return (values ?? new()).Select(c => c.To<PersonDto>());
        }
    }
}
