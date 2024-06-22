using Api.Dtos.Persons;
using Api.Infra;
using Api.Querys;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController
    {
        private readonly Lazy<IPersonQuery> _query;
        private readonly Lazy<IPersonService> _service;

        public PersonController(Lazy<IPersonQuery> personQuery, Lazy<IPersonService> personService)
        {
            _query = personQuery;
            _service = personService;
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

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonDetailTypes()
        {
            var values = _query.Value.GetAllPersonDetailTypes();

            return values.ToResultResponse();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPersonDto dto)
        {
            var person = await _service.Value.Create(dto);

            return person.ToCompleteResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(PersonDto dto)
        {
            var result = await _service.Value.Delete(dto);

            return result.ToCompleteResponse();
        }
    }
}
