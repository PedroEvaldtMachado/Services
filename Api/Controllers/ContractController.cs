using Api.Dtos.Contracts;
using Api.Dtos.Stakeholders;
using Api.Infra;
using Api.Querys;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController
    {
        private readonly Lazy<IContractQuery> _query;
        private readonly Lazy<IContractService> _service;

        public ContractController(Lazy<IContractQuery> query, Lazy<IContractService> service)
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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Request(NewContractDto dto)
        {
            var result = await _service.Value.Request(dto);

            return result.ToCompleteResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Approve(ContractDto dto)
        {
            var result = await _service.Value.Approve(dto);

            return result.ToCompleteResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ApproveWithModifications(ContractDto dto)
        {
            var result = await _service.Value.ApproveWithModifications(dto);

            return result.ToCompleteResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Done(ContractDto dto)
        {
            var result = await _service.Value.Done(dto);

            return result.ToCompleteResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Canceled(ContractDto dto)
        {
            var result = await _service.Value.Canceled(dto);

            return result.ToCompleteResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Reject(ContractDto dto)
        {
            var result = await _service.Value.Reject(dto);

            return result.ToCompleteResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Inactive(ContractDto dto)
        {
            var result = await _service.Value.Inactive(dto);

            return result.ToCompleteResponse();
        }
    }
}
