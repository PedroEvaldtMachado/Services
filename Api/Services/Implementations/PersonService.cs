using Api.Domain.Entities.Persons;
using Api.Dtos.Persons;
using Api.Infra.Enums;
using Api.Infra.Resourses;
using Api.Mappers;
using Api.Querys;
using Api.Repositorys;
using FluentResults;
using MongoDB.Driver;

namespace Api.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private static readonly PersonType[] VALID_PERSON_TYPES = new[] { PersonType.Individual, PersonType.LegalEntity };

        private readonly Lazy<IRepository<Person>> _repository;
        private readonly Lazy<ICountryValidation> _countryValidation;
        private readonly Lazy<IContracteeQuery> _contracteeQuery;

        public PersonService(Lazy<IRepository<Person>> repository, Lazy<ICountryValidation> countryValidation, Lazy<IContracteeQuery> contracteeQuery)
        {
            _repository = repository;
            _countryValidation = countryValidation;
            _contracteeQuery = contracteeQuery;
        }

        public async Task<Result<PersonDto>> Create(NewPersonDto newDto)
        {
            var result = ValidateNewPerson(newDto);

            if (result.IsFailed)
            {
                return result;
            }

            var dto = Mapper.Map(newDto);
            result.WithErrors((await _countryValidation.Value.ValidateCountryForPerson(dto)).Errors);

            if (result.IsFailed)
            {
                return result;
            }

            var ent = Mapper.Map(dto);
            ent.RegisterDate = DateTimeOffset.UtcNow;
            await _repository.Value.Collection.InsertOneAsync(ent);

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<bool>> Delete(PersonDto dto)
        {
            if (dto is null || dto.Id <= 0)
            {
                return false.ToResult().WithError(Message.Get(3));
            }

            var result = await ValidateDelete(dto);

            if (result.IsFailed)
            {
                return result;
            }

            var deletes = await _repository.Value.Collection.DeleteOneAsync(e => e.Id == dto.Id);

            return result.WithValue(deletes.DeletedCount > 0);
        }

        private static Result<PersonDto> ValidateNewPerson(NewPersonDto dto)
        {
            var result = new Result();

            if (dto is null)
            {
                result.WithError(Message.Get(3));
            }
            else
            {
                if (dto.Name is null)
                {
                    result.WithError(Message.Get(4));
                }

                if (!VALID_PERSON_TYPES.Contains(dto.PersonType))
                {
                    result.WithError(Message.Get(7));
                }

                if (string.IsNullOrWhiteSpace(dto.CountryId.ToString()))
                {
                    result.WithError(Message.Get(8));
                }
            }

            return result;
        }

        private async Task<Result<bool>> ValidateDelete(PersonDto dto)
        {
            var contractee = await _contracteeQuery.Value.GetByPersonId(dto.Id);

            if (contractee is not null) 
            {
                return new Result().WithError(Message.Get(15));
            }

            return new Result();
        }
    }
}
