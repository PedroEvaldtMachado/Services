using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Countrys;
using Api.Dtos.Stakeholders;
using Api.Infra.Resourses;
using Api.Mappers;
using Api.Repositorys;
using FluentResults;
using MongoDB.Driver;

namespace Api.Services.Implementations
{
    public class ContracteeService : IContracteeService
    {
        private readonly Lazy<IRepository<Contractee>> _repository;
        private readonly Lazy<IPersonService> _personService;

        public ContracteeService(Lazy<IRepository<Contractee>> repository, Lazy<IPersonService> personService)
        {
            _repository = repository;
            _personService = personService;
        }

        public async Task<Result<ContracteeDto>> Create(NewContracteeDto newDto)
        {
            var result = ValidateNew(newDto);
            if (result.IsFailed)
            {
                return result;
            }

            result.WithErrors((await Duplicated(newDto)).Errors);

            if (result.IsFailed)
            {
                return result;
            }

            if (!newDto.PersonId.HasValue)
            {
                var personResult = await _personService.Value.Create(newDto.Person!);

                if (personResult.IsFailed)
                {
                    return personResult.ToResult<ContracteeDto>();
                }

                newDto.PersonId = personResult.Value.Id;
            }

            var dto = Mapper.Map(newDto);
            var ent = Mapper.Map(dto);
            await _repository.Value.Collection.InsertOneAsync(ent);

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<bool>> Delete(ContracteeDto dto)
        {
            if (dto is null || dto.Id == Guid.Empty)
            {
                return false.ToResult().WithError(Message.Get(3));
            }

            var result = await _repository.Value.Collection.DeleteOneAsync(e => e.Id == dto.Id);

            return (result.DeletedCount > 0).ToResult();
        }

        public async Task<Result<bool>> DeleteByPerson(Guid personId)
        {
            if (personId == Guid.Empty)
            {
                return false.ToResult().WithError(Message.Get(10));
            }

            var result = await _repository.Value.Collection.DeleteOneAsync(e => e.PersonId == personId);

            return (result.DeletedCount > 0).ToResult();
        }

        private async Task<Result<ContracteeDto>> Duplicated(NewContracteeDto dto)
        {
            var result = new Result();

            var existsSameName = await _repository.Value.Collection.FindAsync(e => e.PersonId == dto.PersonId);

            if (await existsSameName.AnyAsync())
            {
                result.WithError(Message.Get(9));
            }

            return result;
        }

        private static Result<ContracteeDto> ValidateNew(NewContracteeDto dto)
        {
            var result = new Result();

            if (dto is null)
            {
                result.WithError(Message.Get(3));
            }
            else
            {
                if (dto.PersonId is null && dto.Person is null)
                {
                    result.WithError(Message.Get(10));
                }
            }

            return result;
        }
    }
}
