using Api.Domain.Entities;
using Api.Dtos.Countrys;

namespace Api.Querys.Implementations
{
    public class CountryQuery : BaseQuery<Country, CountryDto>, ICountryQuery
    {
        public CountryQuery(BaseQueryParams baseParams) : base(baseParams)
        {
        }
    }
}
