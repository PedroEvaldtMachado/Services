using Api.Dtos.Services;
using Api.Infra;
using Api.Querys;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceTypeController
    {
        private readonly Lazy<IServiceTypeQuery> _query;
        private readonly Lazy<IServiceTypeService> _service;

        public ServiceTypeController(Lazy<IServiceTypeQuery> query, Lazy<IServiceTypeService> service)
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
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _query.Value.GetAll();

            return dtos.ToResultResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Search(string value)
        {
            var dto = await _query.Value.Search(value);

            return dto.ToResultResponse();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewServiceTypeDto dto)
        {
            var result = await _service.Value.Create(dto);

            return result.ToCompleteResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ServiceTypeDto dto)
        {
            var result = await _service.Value.Delete(dto);

            return result.ToCompleteResponse();
        }
    }
}
