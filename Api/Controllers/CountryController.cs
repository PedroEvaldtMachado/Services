using Api.Dtos.Countrys;
using Api.Infra;
using Api.Querys;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get(long id)
        {
            var dto = await _query.Value.GetById(id);

            return dto.ToResultResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Search(string value)
        {
            var dto = await _query.Value.Search(value);

            return dto.ToResultResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _query.Value.GetAll();

            return dtos.ToResultResponse();
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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddObligation(ObligationDto dto)
        {
            var result = await _service.Value.AddObligation(dto);

            return result.ToCompleteResponse();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> RemoveObligation(ObligationDto dto)
        {
            var result = await _service.Value.RemoveObligation(dto);

            return result.ToCompleteResponse();
        }
    }
}
