﻿using Api.Domain.Entities.Countrys;
using Api.Dtos.Countrys;
using Api.Infra.Resourses;
using Api.Mappers;
using Api.Querys;
using Api.Repositorys;
using FluentResults;
using MongoDB.Driver;

namespace Api.Services.Implementations
{
    public class CountryService : BaseService, ICountryService
    {
        private readonly Lazy<IRepository<Country>> _repository;
        private readonly Lazy<ICountryQuery> _query;

        public CountryService(
            BaseServiceParams baseServiceParams,
            Lazy<ICountryQuery> query,
            Lazy<IRepository<Country>> repository) : base(baseServiceParams)
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

            result.WithErrors(DuplicateValidation(newDto).Errors);

            if (result.IsFailed)
            {
                return result;
            }

            var dto = Mapper.Map(newDto);
            var ent = Mapper.Map(dto);
            await _repository.Value.InsertAsync(ent);
            await Context.Value.SaveChangesAsync();

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<bool>> Delete(CountryDto dto)
        {
            if (dto is null || dto.Id <= 0)
            {
                return false.ToResult().WithError(Message.Get(3));
            }

            _repository.Value.Delete(Mapper.Map(dto));
            var count = await Context.Value.SaveChangesAsync();

            return (count > 0).ToResult();
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
            _repository.Value.Update(ent);
            await Context.Value.SaveChangesAsync();

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
            _repository.Value.Update(ent);

            var count = await Context.Value.SaveChangesAsync();

            return result.WithValue(Mapper.Map(ent));
        }

        private Result<CountryDto> DuplicateValidation(NewCountryDto dto)
        {
            var result = new Result<CountryDto>();

            var existsSameName = _repository.Value.Queryable.Any(e => e.Name == dto.Name);

            if (existsSameName)
            {
                result.WithError(Message.Get(1));
            }

            var existsSameCode = _repository.Value.Queryable.Any(e => e.Code == dto.Code);

            if (existsSameCode)
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
