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
    public class ContracteeServiceProvideService : IContracteeServiceProvideService
    {
        private readonly Lazy<IRepository<ContracteeServiceProvide>> _repository;
        private readonly Lazy<IContracteeServiceProvideQuery> _query;

        public ContracteeServiceProvideService(Lazy<IContracteeServiceProvideQuery> query, Lazy<IRepository<ContracteeServiceProvide>> repository)
        {
            _query = query;
            _repository = repository;
        }

        public async Task<Result<ContracteeServiceProvideDto>> Create(NewContracteeServiceProvideDto newDto)
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

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<bool>> Delete(ContracteeServiceProvideDto dto)
        {
            if (dto is null || dto.Id <= 0)
            {
                return false.ToResult().WithError(Message.Get(3));
            }

            var result = await _repository.Value.DeleteAsync(Mapper.Map(dto));

            return (result > 0).ToResult();
        }

        private Result<ContracteeServiceProvideDto> DuplicateValidation(NewContracteeServiceProvideDto dto)
        {
            var result = new Result<ContracteeServiceProvideDto>();
            var existsSame = _repository.Value.Queryable.Any(e => e.ServiceTypeId == dto.ServiceTypeId && e.ContracteeId == dto.ContracteeId);

            if (existsSame)
            {
                result.WithError(Message.Get(24));
            }

            return result;
        }

        private static Result<ContracteeServiceProvideDto> ValidateNew(NewContracteeServiceProvideDto dto)
        {
            var result = new Result<ContracteeServiceProvideDto>();

            if (dto is null)
            {
                result.WithError(Message.Get(3));
            }
            else
            {
                if (dto.ServiceTypeId <= 0)
                {
                    result.WithError(Message.Get(22));
                }

                if (dto.ContracteeId <= 0)
                {
                    result.WithError(Message.Get(23));
                }
            }

            return result;
        }
    }
}
