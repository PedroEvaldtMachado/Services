using Api.Domain.Entities.Persons;
using Api.Dtos;
using Api.Dtos.Persons;
using Api.Infra.Enums;
using Api.Mappers;
using MongoDB.Driver;

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

            var query = await Collection.FindAsync(c => c.Name!.ToLower().Contains(value) || c.Username!.ToLower().Contains(value));
            var values = await query.ToListAsync();

            return values.Select(c => c.To<PersonDto>());
        }
    }
}
