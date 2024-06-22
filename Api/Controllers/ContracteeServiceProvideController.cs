using Api.Infra;
using Api.Querys;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContracteeServiceProvideController
    {
        private readonly Lazy<IContracteeServiceProvideQuery> _query;
        private readonly Lazy<IContracteeServiceProvideService> _service;

        public ContracteeServiceProvideController(Lazy<IContracteeServiceProvideQuery> query, Lazy<IContracteeServiceProvideService> service)
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
    }
}
