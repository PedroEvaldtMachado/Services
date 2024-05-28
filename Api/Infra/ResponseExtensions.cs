using Api.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infra
{
    public static class ResponseExtensions
    {
        public static ActionResult ToResponse<T>(this IResult<T> result)
        {
            var resultMapped = Mapper.Map(result);

            if (result.IsSuccess)
            {
                return new OkObjectResult(result.ValueOrDefault);
            }
            else
            {
                return new BadRequestObjectResult(resultMapped);
            }
        }

        public static ActionResult ToCompleteResponse<T>(this IResult<T> result)
        {
            var resultMapped = Mapper.Map(result);

            if (result.IsSuccess)
            {
                return new OkObjectResult(resultMapped);
            }
            else
            {
                return new BadRequestObjectResult(resultMapped);
            }
        }

        public static ActionResult ToCompleteResponse(this IResultBase result)
        {
            var resultMapped = Mapper.Map(result);

            if (result.IsSuccess)
            {
                return new OkObjectResult(resultMapped);
            }
            else
            {
                return new BadRequestObjectResult(resultMapped);
            }
        }
    }
}
