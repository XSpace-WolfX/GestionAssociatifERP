using Asp.Versioning;
using GestionAssociatifERP.Dtos.V1;
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

            return Ok(result);
        }

        // GET: api/v1/linkresponsableenfant/enfant/{enfantId}
        [HttpGet("enfant/{enfantId}")]
        public async Task<IActionResult> GetResponsablesByEnfant(int enfantId)
        {
            var result = await _linkResponsableEnfantService.GetResponsablesByEnfantIdAsync(enfantId);

            return Ok(result);
        }

        // GET: api/v1/linkresponsableenfant/responsable/{responsableId}/enfant/{enfantId}
        [HttpGet("responsable/{responsableId}/enfant/{enfantId}")]
        public async Task<IActionResult> Exists(int responsableId, int enfantId)
        {
            var result = await _linkResponsableEnfantService.ExistsLinkResponsableEnfantAsync(enfantId, responsableId);

            return Ok(result);
        }

        // POST: api/v1/linkresponsableenfant
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLinkResponsableEnfantDto linkResponsableEnfantDto)
        {
            var result = await _linkResponsableEnfantService.CreateLinkResponsableEnfantAsync(linkResponsableEnfantDto);

            return Ok(result);
        }

        // PUT: api/v1/linkresponsableenfant
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLinkResponsableEnfantDto linkResponsableEnfantDto)
        {
            if (linkResponsableEnfantDto == null)
                return BadRequest(new { Message = "Le corps de la requête ne peut pas être vide." });

            await _linkResponsableEnfantService.UpdateLinkResponsableEnfantAsync(linkResponsableEnfantDto);

            return NoContent();
        }

        // DELETE: api/v1/linkresponsableenfant/responsable/{responsableId}/enfant/{enfantId}
        [HttpDelete("responsable/{responsableId}/enfant/{enfantId}")]
        public async Task<IActionResult> Delete(int responsableId, int enfantId)
        {
            await _linkResponsableEnfantService.RemoveLinkResponsableEnfantAsync(enfantId, responsableId);

            return NoContent();
        }
    }
}