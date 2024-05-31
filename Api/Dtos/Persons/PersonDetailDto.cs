using Api.Infra.Enums;

namespace Api.Dtos.Persons
{
    public class PersonDetailDto
    {
        public PersonDetailType PersonDetailType { get; set; }

        public string? Detail { get; set; }
    }
}
