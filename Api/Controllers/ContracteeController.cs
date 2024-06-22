using Api.Dtos.Services;
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
        private readonly Lazy<IContracteeServiceProvideQuery> _contracteeServiceProvideQuery;
        private readonly Lazy<IContracteeServiceProvideService> _contracteeServiceProvideService;

        public ContracteeController(Lazy<IContracteeQuery> query, Lazy<IContracteeService> service, Lazy<IContracteeServiceProvideService> contracteeServiceProvideService, Lazy<IContracteeServiceProvideQuery> contracteeServiceProvideQuery)
        {
            _query = query;
            _service = service;
            _contracteeServiceProvideQuery = contracteeServiceProvideQuery;
            _contracteeServiceProvideService = contracteeServiceProvideService;
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
        public async Task<IActionResult> Create(NewContracteeDto dto)
        {
            var result = await _service.Value.Create(dto);

            return result.ToCompleteResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ContracteeDto dto)
        {
            var result = await _service.Value.Delete(dto);

            return result.ToCompleteResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetServicesProvided(long id)
        {
            var result = await _contracteeServiceProvideQuery.Value.GetByContracteeId(id);

            return result.ToResultResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddServiceProvided(NewContracteeServiceProvideDto dto)
        {
            var result = await _contracteeServiceProvideService.Value.Create(dto);

            return result.ToCompleteResponse();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> RemoveServiceProvided(ContracteeServiceProvideDto dto)
        {
            var result = await _contracteeServiceProvideService.Value.Delete(dto);

            return result.ToCompleteResponse();
        }
    }
}
