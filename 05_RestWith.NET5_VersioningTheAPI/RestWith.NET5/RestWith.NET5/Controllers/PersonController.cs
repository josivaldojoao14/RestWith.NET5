using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWith.NET5.Model;
using RestWith.NET5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWith.NET5.Controllers
{
    [ApiVersion("1")] // Indica qual a versão da API
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")] // Indica qual o path global da API, juntamente com o versionamento que fizemos acima
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        } 
        
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personService.Create(person));
        }         

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personService.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
