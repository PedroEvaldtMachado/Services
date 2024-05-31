using Api.Infra.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Domain.Entities.Persons
{
    public class Person : BaseEntity
    {
        public string? Name { get; set; }

        public string? Username { get; set; }

        public DateTimeOffset RegisterDate { get; set; }

        public Guid CountryId { get; set; }

        public PersonType PersonType { get; set; }

        public ICollection<PersonDetail> PersonDetails { get; set; }

        public Person()
        {
            PersonDetails = new List<PersonDetail>();
        }
    }
}
