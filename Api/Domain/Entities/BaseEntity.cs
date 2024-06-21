using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Domain.Entities
{
    public class BaseEntity
    {
        [BsonId]
        public long Id { get; set; }

        public Guid? EditControl { get; set; }
    }
}
