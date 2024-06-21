using Api.Domain.Entities.Countrys;
using Api.Dtos.Countrys;
using Api.Infra.Resourses;
using Api.Mappers;
using Api.Querys;
using Api.Repositorys;
using FluentResults;
using MongoDB.Driver;

namespace Api.Services.Implementations
{
    public class CountryService : ICountryService
    {
        private readonly Lazy<IRepository<Country>> _repository;
        private readonly Lazy<ICountryQuery> _query;

        public CountryService(Lazy<ICountryQuery> query, Lazy<IRepository<Country>> repository)
        {
            _query = query;
            _repository = repository;
        }

        public async Task<Result<CountryDto>> Create(NewCountryDto newDto)
        {
            var result = ValidateNew(newDto);

            if (result.IsFailed)
            {
                return result;
            }

            result.WithErrors((await DuplicateValidation(newDto)).Errors);

            if (result.IsFailed)
            {
                return result;
            }

            var dto = Mapper.Map(newDto);
            var ent = Mapper.Map(dto);
            await _repository.Value.Collection.InsertOneAsync(ent);

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<bool>> Delete(CountryDto dto)
        {
            if (dto is null || dto.Id <= 0)
            {
                return false.ToResult().WithError(Message.Get(3));
            }

            var result = await _repository.Value.Collection.DeleteOneAsync(e => e.Id == dto.Id);

            return (result.DeletedCount > 0).ToResult();
        }

        public async Task<Result<CountryDto>> AddObligation(ObligationDto obligationDto)
        {
            var result = new Result<CountryDto>();

            var dto = await _query.Value.GetById(obligationDto.CountryId);

            if (dto is null)
            {
                return result.WithError(Message.Get(11));
            }
            else if (dto.CountryObligations.Any(o => o.PersonDetailType == obligationDto.PersonDetailType))
            {
                return result.WithError(Message.Get(13, obligationDto.PersonDetailType.ToString()));
            }

            dto.CountryObligations.Add(Mapper.Map(obligationDto));

            var ent = Mapper.Map(dto);
            await _repository.Value.Collection.ReplaceOneAsync(e => e.Id == ent.Id, ent);

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<CountryDto>> RemoveObligation(ObligationDto obligationDto)
        {
            var result = new Result<CountryDto>();
            var dto = await _query.Value.GetById(obligationDto.CountryId);

            if (dto is null)
            {
                return result.WithError(Message.Get(11));
            }
            else if (!dto.CountryObligations.Any(o => o.PersonDetailType == obligationDto.PersonDetailType))
            {
                return result.WithError(Message.Get(14));
            }

            dto.CountryObligations.Remove(dto.CountryObligations.First(o => o.PersonDetailType == obligationDto.PersonDetailType));

            var ent = Mapper.Map(dto);
            await _repository.Value.Collection.ReplaceOneAsync(e => e.Id == ent.Id, ent);

            return result.WithValue(Mapper.Map(ent));
        }

        private async Task<Result<CountryDto>> DuplicateValidation(NewCountryDto dto)
        {
            var result = new Result<CountryDto>();

            var existsSameName = await _repository.Value.Collection.FindAsync(e => e.Name == dto.Name);

            if (await existsSameName.AnyAsync())
            {
                result.WithError(Message.Get(1));
            }

            var existsSameCode = await _repository.Value.Collection.FindAsync(e => e.Code == dto.Code);

            if (await existsSameCode.AnyAsync())
            {
                result.WithError(Message.Get(2));
            }

            return result;
        }

        private static Result<CountryDto> ValidateNew(NewCountryDto dto)
        {
            var result = new Result<CountryDto>();

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

                if (dto.Code is null)
                {
                    result.WithError(Message.Get(5));
                }
                else
                {
                    if (dto.Code.Length != 3)
                    {
                        result.WithError(Message.Get(6));
                    }
                }
            }

            return result;
        }
    }
}
