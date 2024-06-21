using Api.Domain.Entities.Contracts;
using Api.Domain.Entities.Stakeholders;
using Api.Dtos.Contracts;
using Api.Dtos.Countrys;
using Api.Dtos.Stakeholders;
using Api.Infra.Enums;
using Api.Infra.Resourses;
using Api.Mappers;
using Api.Repositorys;
using FluentResults;
using MongoDB.Driver;

namespace Api.Services.Implementations
{
    public class ContractService : IContractService
    {
        private readonly Lazy<IRepository<Contract>> _repository;

        public ContractService(Lazy<IRepository<Contract>> repository)
        {
            _repository = repository;
        }

        public async Task<Result<ContractDto>> Request(NewContractDto newDto)
        {
            var result = ValidateNew(newDto);
            if (result.IsFailed)
            {
                return result;
            }

            newDto.ContractStatus = ContractStatus.Requested;

            var dto = Mapper.Map(newDto);
            var ent = Mapper.Map(dto);

            await _repository.Value.Collection.InsertOneAsync(ent);

            return result.WithValue(Mapper.Map(ent));
        }

        public async Task<Result<ContractDto>> Approve(ContractDto dto)
        {
            if (dto.ContractStatus is not ContractStatus.Requested or ContractStatus.WaitingContractor)
            {
                return dto.ToResult().WithError(Message.Get(20));
            }

            dto.ContractStatus = ContractStatus.Active;

            var ent = Mapper.Map(dto);

            await _repository.Value.Collection.ReplaceOneAsync(c => c.Id == ent.Id, ent);

            return Mapper.Map(ent).ToResult();
        }

        public async Task<Result<ContractDto>> ApproveWithModifications(ContractDto dto)
        {
            if (dto.ContractStatus is not ContractStatus.Requested or ContractStatus.WaitingContractor)
            {
                return dto.ToResult().WithError(Message.Get(20));
            }

            dto.ContractStatus = NewContractStatusNegotiation(dto.ContractStatus);

            var ent = Mapper.Map(dto);

            await _repository.Value.Collection.ReplaceOneAsync(c => c.Id == ent.Id, ent);

            return Mapper.Map(ent).ToResult();
        }

        private ContractStatus NewContractStatusNegotiation(ContractStatus contractStatus) 
        {
            return (contractStatus) switch
            {
                ContractStatus.Requested => ContractStatus.WaitingContractor,
                ContractStatus.WaitingContractor => ContractStatus.Requested,
                _ => contractStatus,
            };
        }

        public async Task<Result<ContractDto>> Done(ContractDto dto)
        {
            if (dto.ContractStatus is not ContractStatus.Active)
            {
                return dto.ToResult().WithError(Message.Get(20));
            }

            dto.ContractStatus = ContractStatus.Done;

            var ent = Mapper.Map(dto);

            await _repository.Value.Collection.ReplaceOneAsync(c => c.Id == ent.Id, ent);

            //TODO: Update contractee counter

            return Mapper.Map(ent).ToResult();
        }

        public async Task<Result<ContractDto>> Canceled(ContractDto dto)
        {
            if (dto.ContractStatus is not ContractStatus.Active)
            {
                return dto.ToResult().WithError(Message.Get(20));
            }

            dto.ContractStatus = ContractStatus.Canceled;

            var ent = Mapper.Map(dto);

            await _repository.Value.Collection.ReplaceOneAsync(c => c.Id == ent.Id, ent);

            return Mapper.Map(ent).ToResult();
        }

        public async Task<Result<ContractDto>> Reject(ContractDto dto)
        {
            if (dto.ContractStatus is not ContractStatus.Requested or ContractStatus.WaitingContractor)
            {
                return dto.ToResult().WithError(Message.Get(20));
            }

            dto.ContractStatus = ContractStatus.Rejected;

            var ent = Mapper.Map(dto);

            await _repository.Value.Collection.ReplaceOneAsync(c => c.Id == ent.Id, ent);

            return Mapper.Map(ent).ToResult();
        }

        public async Task<Result<ContractDto>> Inactive(ContractDto dto)
        {
            if (dto.ContractStatus is not ContractStatus.Requested or ContractStatus.WaitingContractor)
            {
                return dto.ToResult().WithError(Message.Get(20));
            }

            dto.ContractStatus = ContractStatus.Inactive;

            var ent = Mapper.Map(dto);

            await _repository.Value.Collection.ReplaceOneAsync(c => c.Id == ent.Id, ent);

            return Mapper.Map(ent).ToResult();
        }

        private static Result<ContractDto> ValidateNew(NewContractDto dto)
        {
            var result = new Result();

            if (dto is null)
            {
                result.WithError(Message.Get(3));
            }
            else
            {
                if (dto.ContracteeServiceId <= 0)
                {
                    result.WithError(Message.Get(17));
                }

                if (dto.ContracteeId <= 0)
                {
                    result.WithError(Message.Get(18));
                }

                if (dto.ContractorId <= 0)
                {
                    result.WithError(Message.Get(19));
                }

                if (dto.ContractStatus != ContractStatus.New)
                { 
                    result.WithError(Message.Get(20));
                }
            }

            return result;
        }
    }
}
