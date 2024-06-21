using Api.Domain.Entities;
using Api.Dtos.Countrys;
using Api.Dtos.Persons;
using Api.Dtos;
using FluentResults;
using Riok.Mapperly.Abstractions;
using Api.Domain.Entities.Persons;
using Api.Domain.Entities.Countrys;
using Api.Domain.Entities.Contracts;
using Api.Dtos.Contracts;
using Api.Dtos.Schedulings;
using Api.Domain.Entities.Schedulings;
using Api.Dtos.Stakeholders;
using Api.Domain.Entities.Stakeholders;
using Api.Domain.Entities.Services;
using Api.Dtos.Services;
using Microsoft.OpenApi.Any;
using System.Collections;

namespace Api.Mappers
{
    [Mapper]
    public static partial class Mapper
    {
        public static partial D Map<O, D>(O origin);
        public static partial D To<D>(this object origin);
        public static partial IEnumerable Map(IEnumerable originCollection);
        public static partial ICollection Map(ICollection originCollection);

        public static partial BaseDto Map(BaseEntity ent);
        public static partial BaseEntity Map(BaseDto dto);

        public static partial DefaultResultDto Map(IResultBase resultBase);
        public static partial DefaultResultDto Map<T>(IResult<T> result);
        public static partial DefaultResultDto Map(IResult<object> result);

        public static partial PersonDto Map(Person ent);
        public static partial Person Map(PersonDto dto);
        public static partial PersonDto Map(NewPersonDto dto);
        public static partial PersonDetailDto Map(PersonDetail ent);
        public static partial PersonDetail Map(PersonDetailDto dto);

        public static partial CountryDto Map(Country ent);
        public static partial Country Map(CountryDto dto);
        public static partial CountryDto Map(NewCountryDto dto);
        public static partial CountryObligationsDto Map(CountryObligations ent);
        public static partial CountryObligations Map(CountryObligationsDto dto);
        public static partial CountryObligationsDto Map(ObligationDto dto);

        public static partial ContracteeDto Map(Contractee ent);
        public static partial Contractee Map(ContracteeDto dto);
        public static partial ContracteeDto Map(NewContracteeDto dto);
        public static partial ContractorDto Map(Contractor ent);
        public static partial Contractor Map(ContractorDto dto);
        public static partial ContractorDto Map(NewContractorDto dto);

        public static partial StakeholderRateDto Map(StakeholderRate ent);
        public static partial StakeholderRate Map(StakeholderRateDto dto);

        public static partial ContractDto Map(Contract ent);
        public static partial Contract Map(ContractDto dto);
        public static partial ContractDto Map(NewContractDto dto);
        public static partial ContractDetailDto Map(ContractDetail ent);
        public static partial ContractDetail Map(ContractDetailDto dto);

        public static partial SchedulingDto Map(Scheduling ent);
        public static partial Scheduling Map(SchedulingDto dto);
        public static partial SchedulingDateDto Map(SchedulingDate ent);
        public static partial SchedulingDate Map(SchedulingDateDto dto);

        public static partial ServiceTypeDto Map(ServiceType ent);
        public static partial ServiceType Map(ServiceTypeDto dto);
        public static partial ServiceTypeDto Map(NewServiceTypeDto dto);
        public static partial ContracteeServiceProvideDto Map(ContracteeServiceProvide ent);
        public static partial ContracteeServiceProvide Map(ContracteeServiceProvideDto dto);
        public static partial ContracteeServiceProvideDto Map(NewContracteeServiceProvideDto dto);
        
        public static partial ContracteeServiceDetailDto Map(ContracteeServiceDetail ent);
        public static partial ContracteeServiceDetail Map(ContracteeServiceDetailDto dto);
    }
}
