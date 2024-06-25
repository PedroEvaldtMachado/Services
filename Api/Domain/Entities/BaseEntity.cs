using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class BaseEntity
    {
        [BsonId, Key]
        public long Id { get; set; }

        public Guid? EditControl { get; set; }
    }
}
