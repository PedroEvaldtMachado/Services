using Api.Domain.Entities.Countrys;
using Api.Dtos;
using Api.Dtos.Countrys;
using Api.Mappers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Api.Querys.Implementations
{
    public class CountryQuery : BaseQuery<Country, CountryDto>, ICountryQuery
    {
        public CountryQuery(BaseQueryParams baseParams) : base(baseParams)
        {
        }

        public async Task<IEnumerable<CountryDto>> Search(string value)
        {
            value ??= string.Empty;
            value = value.ToLower();

            var query = await Collection.FindAsync(c => c.Name!.ToLower().Contains(value) || c.Code!.ToLower().Contains(value));
            var values = await query.ToListAsync();

            return values.Select(c => c.To<CountryDto>());
        }
    }
}
