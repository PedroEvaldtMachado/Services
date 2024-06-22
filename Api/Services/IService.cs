using FluentResults;

namespace Api.Services
{
    public interface IService<Dto, NewDto>
    {
        Task<Result<Dto>> Create(NewDto newDto);
        Task<Result<bool>> Delete(Dto dto);
    }

    public interface IService<D> : IService<D, D>;
}
