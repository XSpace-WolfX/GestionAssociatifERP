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
    public class LinkPersonneAutoriseeEnfantController : ControllerBase
    {
        private readonly ILinkPersonneAutoriseeEnfantService _linkPersonneAutoriseeEnfantService;

        public LinkPersonneAutoriseeEnfantController(ILinkPersonneAutoriseeEnfantService linkPersonneAutoriseeEnfantService)
        {
            _linkPersonneAutoriseeEnfantService = linkPersonneAutoriseeEnfantService;
        }

        // GET: api/v1/linkpersonneautoriseeenfant/personne-autorisee/{personneAutoriseeId}
        [HttpGet("personne-autorisee/{personneAutoriseeId}")]
        public async Task<IActionResult> GetEnfantsByPersonneAutorisee(int personneAutoriseeId)
        {
            var result = await _linkPersonneAutoriseeEnfantService.GetEnfantsByPersonneAutoriseeIdAsync(personneAutoriseeId);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // GET: api/v1/linkpersonneautoriseeenfant/enfant/{enfantId}
        [HttpGet("enfant/{enfantId}")]
        public async Task<IActionResult> GetPersonnesByEnfant(int enfantId)
        {
            var result = await _linkPersonneAutoriseeEnfantService.GetPersonnesAutoriseesByEnfantIdAsync(enfantId);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // GET: api/v1/linkpersonneautoriseeenfant/personne/{personneId}/enfant/{enfantId}
        [HttpGet("personne-autorisee/{personneAutoriseeId}/enfant/{enfantId}")]
        public async Task<IActionResult> Exists(int personneAutoriseeId, int enfantId)
        {
            var result = await _linkPersonneAutoriseeEnfantService.ExistsLinkPersonneAutoriseeEnfantAsync(enfantId, personneAutoriseeId);
            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(result.Data);
        }

        // POST: api/v1/linkpersonneautoriseeenfant
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLinkPersonneAutoriseeEnfantDto linkPersonneAutoriseeEnfantDto)
        {
            var result = await _linkPersonneAutoriseeEnfantService.CreateLinkPersonneAutoriseeEnfantAsync(linkPersonneAutoriseeEnfantDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });
            else if (!result.Success && result.ErrorType == ServiceErrorType.Conflict)
                return Conflict(new { result.Message });
            else if (!result.Success && result.ErrorType == ServiceErrorType.InternalError)
                return StatusCode(500, new
                {
                    MessageContent = result.Message
                });

            return Ok(result.Data);
        }

        // PUT: api/v1/linkpersonneautoriseeenfant
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLinkPersonneAutoriseeEnfantDto linkPersonneAutoriseeEnfantDto)
        {
            if (linkPersonneAutoriseeEnfantDto == null)
                return BadRequest(new { Message = "Le corps de la requête ne peut pas être vide." });

            var result = await _linkPersonneAutoriseeEnfantService.UpdateLinkPersonneAutoriseeEnfantAsync(linkPersonneAutoriseeEnfantDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }

        // DELETE: api/v1/linkpersonneautoriseeenfant/personne/{personneId}/enfant/{enfantId}
        [HttpDelete("personne-autorisee/{personneAutoriseeId}/enfant/{enfantId}")]
        public async Task<IActionResult> Delete(int personneAutoriseeId, int enfantId)
        {
            var result = await _linkPersonneAutoriseeEnfantService.RemoveLinkPersonneAutoriseeEnfantAsync(enfantId, personneAutoriseeId);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }
    }
}