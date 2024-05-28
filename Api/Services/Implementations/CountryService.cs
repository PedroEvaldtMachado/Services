using Api.Domain.Entities;
using Api.Dtos;
using Api.Dtos.Countrys;
using Api.Querys;
using FluentResults;
using MongoDB.Driver;

namespace Api.Services.Implementations
{
    public class CountryService : BaseService<Country, CountryDto>, ICountryService
    {
        private readonly Lazy<ICountryQuery> _query;
        public CountryService(BaseServiceParams baseParams, Lazy<ICountryQuery> query) : base(baseParams)
        {
            _query = query;
        }

        public async Task<Result<CountryDto>> Create(NewCountryDto dto)
        {
            var result = ValidateNewCountry(dto);

            if (result.IsFailed)
            {
                return result;
            }

            result.WithErrors((await Duplicated(dto)).Errors);

            if (result.IsFailed)
            {
                return result;
            }

            var ent = Mapper.Map(dto);
            await Collection.InsertOneAsync(ent);

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<bool>> Delete(CountryDto dto)
        {
            if (dto is null || dto.Id == Guid.Empty )
            {
                return false.ToResult().WithError("Do not filled.");
            }

            var result = await Collection.DeleteOneAsync(e => e.Id == dto.Id);

            return (result.DeletedCount > 0).ToResult();
        }

        private async Task<Result<CountryDto>> Duplicated(NewCountryDto dto)
        {
            var result = new Result<CountryDto>();

            var existsSameName = await Collection.FindAsync(e => e.Name == dto.Name);

            if (await existsSameName.AnyAsync())
            {
                result.WithError("Already exist a country with this name.");
            }

            var existsSameCode = await Collection.FindAsync(e => e.Code == dto.Code);

            if (await existsSameCode.AnyAsync())
            {
                result.WithError("Already exist a country with this code.");
            }

            return result;
        }

        private static Result<CountryDto> ValidateNewCountry(NewCountryDto dto)
        {
            var result = new Result<CountryDto>();

            if (dto is null)
            {
                result.WithError("Do not filled.");
            }
            else 
            {
                if (dto.Name is null)
                {
                    result.WithError("Name do not filled.");
                }

                if (dto.Code is null)
                {
                    result.WithError("Code do not filled.");
                }
                else
                {
                    if (dto.Code.Length != 3)
                    {
                        result.WithError("Code length must be 3 (ALPHA-3).");
                    }
                }
            }

            return result;
        }
    }
}
