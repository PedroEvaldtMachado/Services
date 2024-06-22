using Api.Dtos;
using Api.Mappers;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infra
{
    public static class ResponseExtensions
    {
        public static ActionResult ToResultResponse<T>(this T? result)
        {
            return result.ToResult().ToResponse();
        }

        public static ActionResult ToResultCompleteResponse<T>(this T? result)
        {
            return result.ToResult().ToCompleteResponse();
        }

        public static ActionResult ToResponse<T>(this IResult<T> result)
        {
            var resultMapped = ConvertResultToDefault(result);

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

            var resultMapped = ConvertResultToDefault(result);

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

        private static DefaultResultDto ConvertResultToDefault<T>(IResult<T> result)
        {
            DefaultResultDto resultMapped;
            var type = typeof(T);

            if (type.IsPrimitive || type == typeof(Decimal) || type == typeof(String) || type == typeof(DateTime) || type == typeof(DateTimeOffset))
            {
                resultMapped = Mapper.Map((IResultBase)result);
                resultMapped.ValueOrDefault = resultMapped.ValueOrDefault;
            }
            else
            {
                resultMapped = Mapper.Map(result);
            }

            return resultMapped;
        }
    }
}
