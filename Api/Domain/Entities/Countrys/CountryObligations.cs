using Api.Infra.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Domain.Entities.Countrys
{
    public class CountryObligations : BaseEntity
    {
        [BsonIgnore]
        public long CountryId { get; set; }

        public PersonDetailType PersonDetailType { get; set; }

        public bool Required { get; set; }
    }
}
