using Api.Domain.Entities;
using Api.Dtos.Countrys;

namespace Api.Querys
{
    public interface ICountryQuery : IQuery<Country, CountryDto>
    {
    }
}
