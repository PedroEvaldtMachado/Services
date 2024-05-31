using Api.Dtos.Countrys;
using FluentResults;

namespace Api.Services
{
    public interface ICountryService : IService<CountryDto, NewCountryDto>
    {
        Task<Result<CountryDto>> AddObligation(ObligationDto obligationDto);
        Task<Result<CountryDto>> RemoveObligation(ObligationDto obligationDto);
    }
}
