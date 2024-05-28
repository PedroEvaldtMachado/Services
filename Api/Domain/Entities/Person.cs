using Api.Infra.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string? Name { get; set; }

        [BsonDateTimeOptions(DateOnly = true)]
        public DateOnly Date { get; set; }

        public int Country { get; set; }

        public PersonType PersonType { get; set; }
    }
}
