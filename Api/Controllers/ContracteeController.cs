using Api.Dtos.Stakeholders;
using Api.Infra;
using Api.Querys;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContracteeController
    {
        private readonly Lazy<IContracteeQuery> _query;
        private readonly Lazy<IContracteeService> _service;

        public ContracteeController(Lazy<IContracteeQuery> query, Lazy<IContracteeService> service)
        {
            _query = query;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _query.Value.GetById(id);

            return result.ToResultResponse();
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
        public async Task<IActionResult> Create(NewContracteeDto dto)
        {
            var person = await _service.Value.Create(dto);

            return person.ToCompleteResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ContracteeDto dto)
        {
            var result = await _service.Value.Delete(dto);

            return result.ToCompleteResponse();
        }
    }
}
