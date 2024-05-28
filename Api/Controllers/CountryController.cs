using Api.Dtos.Countrys;
using Api.Querys;
using Api.Querys.Implementations;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Api.Infra;
using FluentResults;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController
    {
        private readonly Lazy<ICountryQuery> _query;
        private readonly Lazy<ICountryService> _service;

        public CountryController(Lazy<ICountryQuery> query, Lazy<ICountryService> service)
        {
            _query = query;
            _service = service;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get(Guid id)
        {
            var dto = await _query.Value.GetById(id);

            return dto.ToResult().ToCompleteResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _query.Value.GetAll();

            return dtos.ToResult().ToCompleteResponse();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewCountryDto dto)
        {
            var result = await _service.Value.Create(dto);

            return result.ToCompleteResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(CountryDto dto)
        {
            var result = await _service.Value.Delete(dto);

            return result.ToCompleteResponse();
        }
    }
}
