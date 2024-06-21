using Api.Dtos.Stakeholders;
using Api.Infra;
using Api.Querys;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractorController
    {
        private readonly Lazy<IContractorQuery> _query;
        private readonly Lazy<IContractorService> _service;

        public ContractorController(Lazy<IContractorQuery> query, Lazy<IContractorService> service)
        {
            _query = query;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long id)
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
        public async Task<IActionResult> Create(NewContractorDto dto)
        {
            var result = await _service.Value.Create(dto);

            return result.ToCompleteResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ContractorDto dto)
        {
            var result = await _service.Value.Delete(dto);

            return result.ToCompleteResponse();
        }
    }
}
