using Asp.Versioning;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionAssociatifERP.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ResponsablesController : ControllerBase
    {
        private readonly IResponsableService _responsableService;

        public ResponsablesController(IResponsableService responsableService)
        {
            _responsableService = responsableService;
        }

        // GET: api/v1/responsables
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _responsableService.GetAllResponsablesAsync();

            return Ok(result.Data);
        }

        // GET: api/v1/responsables/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _responsableService.GetResponsableAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // GET: api/v1/responsables/{id}/with-enfants
        [HttpGet("{id}/with-enfants")]
        public async Task<IActionResult> GetWithEnfants(int id)
        {
            var result = await _responsableService.GetResponsableWithEnfantsAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // GET: api/v1/responsables/{id}/with-information-financiere
        [HttpGet("{id}/with-information-financiere")]
        public async Task<IActionResult> GetWithInformationFinanciere(int id)
        {
            var result = await _responsableService.GetResponsableWithInformationFinanciereAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // GET: api/v1/responsables/{id}/with-situation-personnelle
        [HttpGet("{id}/with-situation-personnelle")]
        public async Task<IActionResult> GetWithSituationPersonnelle(int id)
        {
            var result = await _responsableService.GetResponsableWithSituationPersonnelleAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // POST: api/v1/responsables
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateResponsableDto responsableDto)
        {
            if (responsableDto == null)
                return BadRequest(new { Message = "Le corps de la requête ne peut pas être vide." });

            var result = await _responsableService.CreateResponsableAsync(responsableDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.InternalError)
                return StatusCode(500, new
                {
                    MessageContent = result.Message
                });

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/v1/responsables/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateResponsableDto responsableDto)
        {
            var result = await _responsableService.UpdateResponsableAsync(id, responsableDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.BadRequest)
                return BadRequest(new { result.Message });
            else if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }

        // DELETE: api/v1/responsables/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _responsableService.DeleteResponsableAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }
    }
}