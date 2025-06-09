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
    public class LinkResponsableEnfantController : ControllerBase
    {
        private readonly ILinkResponsableEnfantService _linkResponsableEnfantService;

        public LinkResponsableEnfantController(ILinkResponsableEnfantService linkResponsableEnfantService)
        {
            _linkResponsableEnfantService = linkResponsableEnfantService;
        }

        // GET: api/v1/linkresponsableenfant/responsable/{responsableId}
        [HttpGet("responsable/{responsableId}")]
        public async Task<IActionResult> GetEnfantsByResponsable(int responsableId)
        {
            var result = await _linkResponsableEnfantService.GetEnfantsByResponsableIdAsync(responsableId);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // GET: api/v1/linkresponsableenfant/enfant/{enfantId}
        [HttpGet("enfant/{enfantId}")]
        public async Task<IActionResult> GetResponsablesByEnfant(int enfantId)
        {
            var result = await _linkResponsableEnfantService.GetResponsablesByEnfantIdAsync(enfantId);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // GET: api/v1/linkresponsableenfant/responsable/{responsableId}/enfant/{enfantId}
        [HttpGet("responsable/{responsableId}/enfant/{enfantId}")]
        public async Task<IActionResult> Exists(int responsableId, int enfantId)
        {
            var result = await _linkResponsableEnfantService.ExistsLinkResponsableEnfantAsync(enfantId, responsableId);
            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(result.Data);
        }

        // POST: api/v1/linkresponsableenfant
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLinkResponsableEnfantDto linkResponsableEnfantDto)
        {
            var result = await _linkResponsableEnfantService.CreateLinkResponsableEnfantAsync(linkResponsableEnfantDto);
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

        // PUT: api/v1/linkresponsableenfant
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLinkResponsableEnfantDto linkResponsableEnfantDto)
        {
            if (linkResponsableEnfantDto == null)
                return BadRequest(new { Message = "Le corps de la requête ne peut pas être vide." });

            var result = await _linkResponsableEnfantService.UpdateLinkResponsableEnfantAsync(linkResponsableEnfantDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }

        // DELETE: api/v1/linkresponsableenfant/responsable/{responsableId}/enfant/{enfantId}
        [HttpDelete("responsable/{responsableId}/enfant/{enfantId}")]
        public async Task<IActionResult> Delete(int responsableId, int enfantId)
        {
            var result = await _linkResponsableEnfantService.RemoveLinkResponsableEnfantAsync(enfantId, responsableId);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }
    }
}