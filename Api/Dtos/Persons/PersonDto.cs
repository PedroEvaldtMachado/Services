using MongoDB.Bson.Serialization.Attributes;

namespace Api.Dtos.Persons
{
    public class PersonDto : BaseDto
    {
        public string? Name { get; set; }

        public DateOnly Date { get; set; }
    }
}
