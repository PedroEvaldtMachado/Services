using FluentResults;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos
{
    public class DefaultResultDto
    {
        public bool IsSuccess { get; set; }
        public bool IsFailed { get; set; }
        public List<ISuccess> Successes { get; set; }
        public List<IError> Errors { get; set; }

        [JsonIgnore]
        public object? ValueOrDefault { get; set; }

        [MapperIgnore]
        public object? Value { get { return ValueOrDefault; } }

        public DefaultResultDto() 
        {
            Successes = new List<ISuccess>();
            Errors = new List<IError>();
        }
    }
}
