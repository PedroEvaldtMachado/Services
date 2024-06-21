using Api.Infra.Enums;

namespace Api.Dtos.Persons
{
    public class PersonDto : BaseDto
    {
        public string? Name { get; set; }

        public string? Username { get; set; }

        public DateTimeOffset RegisterDate { get; set; }

        public long CountryId { get; set; }

        public PersonType PersonType { get; set; }

        public ICollection<PersonDetailDto> PersonDetails { get; set; }

        public PersonDto()
        {
            PersonDetails = new List<PersonDetailDto>();
        }
    }
}
