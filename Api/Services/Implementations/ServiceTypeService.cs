using Api.Domain.Entities.Services;
using Api.Dtos.Services;
using Api.Infra.Resourses;
using Api.Mappers;
using Api.Querys;
using Api.Repositorys;
using FluentResults;
using MongoDB.Driver;

namespace Api.Services.Implementations
{
    public class ServiceTypeService : BaseService, IServiceTypeService
    {
        private readonly Lazy<IRepository<ServiceType>> _repository;
        private readonly Lazy<IServiceTypeQuery> _query;

        public ServiceTypeService(
            BaseServiceParams baseServiceParams,
            Lazy<IServiceTypeQuery> query,
            Lazy<IRepository<ServiceType>> repository) : base(baseServiceParams)
        {
            _query = query;
            _repository = repository;
        }

        public async Task<Result<ServiceTypeDto>> Create(NewServiceTypeDto newDto)
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

        public async Task<Result<bool>> Delete(ServiceTypeDto dto)
        {
            if (dto is null || dto.Id <= 0)
            {
                return false.ToResult().WithError(Message.Get(3));
            }

            _repository.Value.Delete(Mapper.Map(dto));
            var count = await Context.Value.SaveChangesAsync();

            return (count > 0).ToResult();
        }

        private Result<ServiceTypeDto> DuplicateValidation(NewServiceTypeDto dto)
        {
            var result = new Result<ServiceTypeDto>();
            var existsSameName = _query.Value.Queryable.Any(e => e.Name == dto.Name);

            if (existsSameName)
            {
                result.WithError(Message.Get(16));
            }

            return result;
        }

        private static Result<ServiceTypeDto> ValidateNew(NewServiceTypeDto dto)
        {
            var result = new Result<ServiceTypeDto>();

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
            }

            return result;
        }
    }
}
