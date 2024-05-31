using Api.Domain.Entities.Countrys;
using Api.Dtos.Countrys;

namespace Api.Querys
{
    public interface ICountryQuery : IQuery<Country, CountryDto>
    {
        Task<IEnumerable<CountryDto>> Search(string value);
    }
}
