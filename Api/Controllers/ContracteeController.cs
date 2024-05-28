using Api.Querys;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContracteeController
    {
        private readonly Lazy<IPersonQuery> _query;

        public ContracteeController(Lazy<IPersonQuery> query)
        {
            _query = query;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id) 
        {
            return new OkObjectResult(await _query.Value.GetById(id));
        }
    }
}
