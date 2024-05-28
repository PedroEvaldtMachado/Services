using Api.Domain.Entities;
using Api.Dtos.Countrys;
using Api.Dtos.Persons;
using FluentResults;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos
{
    [Mapper]
    [UseStaticMapper<Mapper>]
    public partial class Mapper
    {
        public static partial D Map<O, D>(O origin);

        public static partial BaseEntity Map(BaseDto dto);
        public static partial BaseDto Map(BaseEntity ent);

        public static partial DefaultResultDto Map(IResultBase resultBase);
        public static partial DefaultResultDto Map<T>(IResult<T> result);
        public static partial DefaultResultDto Map(IResult<object> result);

        public static partial Person Map(PersonDto dto);
        public static partial PersonDto Map(Person ent);

        public static partial Country Map(CountryDto dto);
        public static partial CountryDto Map(Country ent);
        public static partial Country Map(NewCountryDto dto);
    }
}
