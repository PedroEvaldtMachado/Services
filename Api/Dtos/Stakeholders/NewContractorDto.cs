using Api.Dtos.Persons;

namespace Api.Dtos.Stakeholders
{
    public class NewContractorDto
    {
        public Guid? PersonId { get; set; }

        public NewPersonDto? Person { get; set; }
    }
}
