using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Stakeholders;
using Api.Infra;
using Api.Infra.Resourses;
using Api.Mappers;
using Api.Querys;
using Api.Repositorys;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Implementations
{
    public class ContracteeService : BaseService, IContracteeService
    {
        private readonly Lazy<IContracteeQuery> _query;
        private readonly Lazy<IRepository<Contractee>> _repository;
        private readonly Lazy<IPersonService> _personService;

        public ContracteeService(
            BaseServiceParams baseServiceParams,
            Lazy<IRepository<Contractee>> repository,
            Lazy<IPersonService> personService,
            Lazy<IContracteeQuery> contracteeQuery) : base(baseServiceParams)
        {
            _repository = repository;
            _personService = personService;
            _query = contracteeQuery;
        }

        public async Task<Result<ContracteeDto>> Create(NewContracteeDto newDto)
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
            await _repository.Value.InsertAsync(ent);
            await Context.Value.SaveChangesAsync();

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<bool>> Delete(ContracteeDto dto)
        {
            if (dto is null || dto.Id <= 0)
            {
                return false.ToResult().WithError(Message.Get(3));
            }


            _repository.Value.Delete(Mapper.Map(dto));

            var count = await Context.Value.SaveChangesAsync();

            return (count > 0).ToResult();
        }

        public async Task<Result<bool>> DeleteByPerson(long personId)
        {
            if (personId <= 0)
            {
                return false.ToResult().WithError(Message.Get(10));
            }

            var contractee = await _repository.Value.Queryable.FirstOrDefaultTryAsync(c => c.PersonId == personId);

            if (contractee is null)
            {
                return false.ToResult().WithError(Message.Get(10));
            }

            _repository.Value.Delete(contractee);

            var count = await Context.Value.SaveChangesAsync();

            return (count > 0).ToResult();
        }

        private async Task<Result<ContracteeDto>> DuplicateValidation(NewContracteeDto dto)
        {
            var result = new Result();

            var existsWithSamePerson = await _query.Value.Queryable.AnyAsync(q => q.PersonId == dto.PersonId!.Value);

            if (existsWithSamePerson)
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
                if (dto.PersonId is null or <= 0 && dto.Person is null)
                {
                    result.WithError(Message.Get(10));
                }
            }

            return result;
        }
    }
}
