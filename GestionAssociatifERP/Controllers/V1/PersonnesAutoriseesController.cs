using Asp.Versioning;
using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionAssociatifERP.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonnesAutoriseesController : ControllerBase
    {
        private readonly IPersonneAutoriseeService _personneAutoriseeService;

        public PersonnesAutoriseesController(IPersonneAutoriseeService personneAutoriseeService, IMapper mapper)
        {
            _personneAutoriseeService = personneAutoriseeService;
        }

        // GET: api/v1/personnesautorisees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _personneAutoriseeService.GetAllPersonnesAutoriseesAsync();

            return Ok(result.Data);
        }

        // GET: api/v1/personnesautorisees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _personneAutoriseeService.GetPersonneAutoriseeAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // GET: api/v1/personnesautorisees/{id}/with-enfants
        [HttpGet("{id}/with-enfants")]
        public async Task<IActionResult> GetWithEnfants(int id)
        {
            var result = await _personneAutoriseeService.GetPersonneAutoriseeWithEnfantsAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // POST: api/v1/personnesautorisees
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonneAutoriseeDto personneAutoriseeDto)
        {
            if (personneAutoriseeDto == null)
                return BadRequest(new { Message = "Le corps de la requête ne peut pas être vide." });

            var result = await _personneAutoriseeService.CreatePersonneAutoriseeAsync(personneAutoriseeDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.InternalError)
                return StatusCode(500, new
                {
                    MessageContent = result.Message
                });

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/v1/personnesautorisees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonneAutoriseeDto personneAutoriseeDto)
        {
            var result = await _personneAutoriseeService.UpdatePersonneAutoriseeAsync(id, personneAutoriseeDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.BadRequest)
                return BadRequest(new { result.Message });
            else if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }

        // DELETE: api/v1/personnesautorisees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personneAutoriseeService.DeletePersonneAutoriseeAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }
    }
}