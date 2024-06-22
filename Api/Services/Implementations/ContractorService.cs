using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Stakeholders;
using Api.Infra;
using Api.Infra.Resourses;
using Api.Mappers;
using Api.Repositorys;
using FluentResults;

namespace Api.Services.Implementations
{
    public class ContractorService : IContractorService
    {
        private readonly Lazy<IRepository<Contractor>> _repository;
        private readonly Lazy<IPersonService> _personService;

        public ContractorService(Lazy<IRepository<Contractor>> repository, Lazy<IPersonService> personService)
        {
            _repository = repository;
            _personService = personService;
        }

        public async Task<Result<ContractorDto>> Create(NewContractorDto newDto)
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
                    return personResult.ToResult<ContractorDto>();
                }

                newDto.PersonId = personResult.Value.Id;
            }

            var dto = Mapper.Map(newDto);
            var ent = Mapper.Map(dto);
            await _repository.Value.InsertAsync(ent);

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<bool>> Delete(ContractorDto dto)
        {
            if (dto is null || dto.Id <= 0)
            {
                return false.ToResult().WithError(Message.Get(3));
            }

            var result = await _repository.Value.DeleteAsync(Mapper.Map(dto));

            return (result > 0).ToResult();
        }

        public async Task<Result<bool>> DeleteByPerson(long personId)
        {
            if (personId <= 0)
            {
                return false.ToResult().WithError(Message.Get(10));
            }

            var contractor = await _repository.Value.Queryable.FirstOrDefaultTryAsync(c => c.PersonId == personId);

            if (contractor is null)
            {
                return false.ToResult().WithError(Message.Get(10));
            }


            var result = await _repository.Value.DeleteAsync(contractor);

            return (result > 0).ToResult();
        }

        private async Task<Result<ContractorDto>> DuplicateValidation(NewContractorDto dto)
        {
            var result = new Result();

            var existsWithSameName = await _repository.Value.FindAsync(dto.PersonId!.Value);

            if (existsWithSameName is not null)
            {
                result.WithError(Message.Get(9));
            }

            return result;
        }

        private static Result<ContractorDto> ValidateNew(NewContractorDto dto)
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
                    result.WithError(Message.Get(21));
                }
            }

            return result;
        }
    }
}
