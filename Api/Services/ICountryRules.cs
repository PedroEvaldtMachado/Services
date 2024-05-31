using Api.Dtos.Persons;
using FluentResults;

namespace Api.Services
{
    public interface ICountryRules
    {
        Task<Result> ValidatePerson(PersonDto person);
    }
}
