using Api.Domain.Entities.Countrys;
using Api.Dtos.Countrys;
using Api.Infra;
using Api.Mappers;

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

            var values = await Queryable.Where(c => c.Name!.ToLower().Contains(value) || c.Code!.ToLower().Contains(value)).ToListTryAsync();

            return (values ?? new()).Select(c => c.To<CountryDto>());
        }
    }
}
