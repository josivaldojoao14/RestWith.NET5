using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWith.NET5.Business;
using RestWith.NET5.Data.VO;
using RestWith.NET5.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestWith.NET5.Controllers
{
    [ApiVersion("1")] // Indica qual a versão da API
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")] // Indica qual o path global da API, juntamente com o versionamento que fizemos acima
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
            _logger = logger;
        }

        [HttpGet]
        // Dentro do swagger o GET poderá produzir vários STATUS, como o "200 (ok)", 
        // como esse GET é responsável por retornar todos os clientes, ele retornará uma lista deles
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))] 
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        } 
        
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))] // Retorna apenas uma pessoa, pq esse GET é por ID
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))] // Retorna apenas uma pessoa, pq o POST é apenas de UM usuario
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }         

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // Retorna apenas o 204 pq é NO CONTENT, após apagar um user, não há retorno de dados
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
