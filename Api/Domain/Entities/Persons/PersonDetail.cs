using Api.Infra.Enums;

namespace Api.Domain.Entities.Persons
{
    public class PersonDetail
    {
        public PersonDetailType PersonDetailType { get; set; }

        public string? Detail { get; set; }
    }
}
