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
    public class SituationsPersonnellesController : ControllerBase
    {
        private readonly ISituationPersonnelleService _situationPersonnelleService;

        public SituationsPersonnellesController(ISituationPersonnelleService situationPersonnelleService)
        {
            _situationPersonnelleService = situationPersonnelleService;
        }

        // GET: api/situationspersonnelles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _situationPersonnelleService.GetAllSituationsPersonnellesAsync();

            return Ok(result.Data);
        }

        // GET: api/situationspersonnelles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _situationPersonnelleService.GetSituationPersonnelleAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // POST: api/situationspersonnelles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSituationPersonnelleDto situationPersonnelleDto)
        {
            if (situationPersonnelleDto == null)
                return BadRequest(new { Message = "Le corps de la requête ne peut pas être vide." });

            var result = await _situationPersonnelleService.CreateSituationPersonnelleAsync(situationPersonnelleDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.InternalError)
                return StatusCode(500, new
                {
                    MessageContent = result.Message
                });

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/situationspersonnelles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSituationPersonnelleDto situationPersonnelleDto)
        {
            var result = await _situationPersonnelleService.UpdateSituationPersonnelleAsync(id, situationPersonnelleDto);

            if (!result.Success && result.ErrorType == ServiceErrorType.BadRequest)
                return BadRequest(new { result.Message });
            else if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }

        // DELETE: api/situationspersonnelles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _situationPersonnelleService.DeleteSituationPersonnelleAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }
    }
}
