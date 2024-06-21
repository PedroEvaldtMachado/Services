using Api.Dtos.Persons;

namespace Api.Dtos.Stakeholders
{
    public class NewContracteeDto
    {
        public long? PersonId { get; set; }

        public NewPersonDto? Person { get; set; }
    }
}
