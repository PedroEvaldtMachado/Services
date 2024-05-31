using Api.Infra.Enums;

namespace Api.Domain.Entities.Countrys
{
    public class CountryObligations
    {
        public PersonDetailType PersonDetailType { get; set; }

        public bool Required { get; set; }
    }
}
