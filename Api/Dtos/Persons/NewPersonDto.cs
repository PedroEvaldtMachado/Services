using Api.Infra.Enums;

namespace Api.Dtos.Persons
{
    public class NewPersonDto
    {
        public string? Name { get; set; }

        public string? Username { get; set; }

        public long CountryId { get; set; }

        public PersonType PersonType { get; set; }

        public ICollection<PersonDetailDto> PersonDetails { get; set; }

        public NewPersonDto()
        {
            PersonDetails = new List<PersonDetailDto>();
        }
    }
}
