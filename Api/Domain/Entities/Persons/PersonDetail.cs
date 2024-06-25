using Api.Infra.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Domain.Entities.Persons
{
    public class PersonDetail : BaseEntity
    {
        [BsonIgnore]
        public long PersonId { get; set; }

        public PersonDetailType PersonDetailType { get; set; }

        public string? Detail { get; set; }
    }
}
