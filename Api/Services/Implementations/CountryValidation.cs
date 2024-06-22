using Api.Dtos.Persons;
using Api.Infra.Resourses;
using Api.Querys;
using FluentResults;

namespace Api.Services.Implementations
{
    public class CountryValidation : ICountryValidation
    {
        private readonly Lazy<ICountryQuery> _countryQuery;

        public CountryValidation(Lazy<ICountryQuery> countryQuery)
        {
            _countryQuery = countryQuery;
        }

        public async Task<Result> ValidateCountryForPerson(PersonDto person)
        {
            var result = new Result();

            var country = await _countryQuery.Value.GetById(person.CountryId);

            if (country == null)
            {
                return result.WithError(Message.Get(11));
            }

            foreach (var obligation in country.CountryObligations.Where(o => o.Required))
            {
                var personDetail = person.PersonDetails.FirstOrDefault(d => d.PersonDetailType == obligation.PersonDetailType);

                if (personDetail is null)
                {
                    result.WithError(Message.Get(12, obligation.PersonDetailType.ToString()));
                }
            }

            return result;
        }
    }
}
