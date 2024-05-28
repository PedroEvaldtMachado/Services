using Api.Dtos.Countrys;
using FluentResults;

namespace Api.Services
{
    public interface ICountryService
    {
        Task<Result<CountryDto>> Create(NewCountryDto dto);
        Task<Result<bool>> Delete(CountryDto dto);
    }
}
