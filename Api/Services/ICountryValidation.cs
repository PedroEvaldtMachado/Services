using Api.Dtos.Persons;
using FluentResults;

namespace Api.Services
{
    public interface ICountryValidation
    {
        Task<Result> ValidateCountryForPerson(PersonDto person);
    }
}
