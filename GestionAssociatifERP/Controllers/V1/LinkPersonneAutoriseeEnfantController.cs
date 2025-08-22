using Asp.Versioning;
using GestionAssociatifERP.Dtos.V1;
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

            return Ok(result);
        }

        // GET: api/v1/linkpersonneautoriseeenfant/enfant/{enfantId}
        [HttpGet("enfant/{enfantId}")]
        public async Task<IActionResult> GetPersonnesByEnfant(int enfantId)
        {
            var result = await _linkPersonneAutoriseeEnfantService.GetPersonnesAutoriseesByEnfantIdAsync(enfantId);

            return Ok(result);
        }

        // GET: api/v1/linkpersonneautoriseeenfant/personne/{personneId}/enfant/{enfantId}
        [HttpGet("personne-autorisee/{personneAutoriseeId}/enfant/{enfantId}")]
        public async Task<IActionResult> Exists(int personneAutoriseeId, int enfantId)
        {
            var result = await _linkPersonneAutoriseeEnfantService.ExistsLinkPersonneAutoriseeEnfantAsync(enfantId, personneAutoriseeId);

            return Ok(result);
        }

        // POST: api/v1/linkpersonneautoriseeenfant
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLinkPersonneAutoriseeEnfantDto linkPersonneAutoriseeEnfantDto)
        {
            var result = await _linkPersonneAutoriseeEnfantService.CreateLinkPersonneAutoriseeEnfantAsync(linkPersonneAutoriseeEnfantDto);

            return Ok(result);
        }

        // PUT: api/v1/linkpersonneautoriseeenfant
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLinkPersonneAutoriseeEnfantDto linkPersonneAutoriseeEnfantDto)
        {
            if (linkPersonneAutoriseeEnfantDto == null)
                return BadRequest(new { Message = "Le corps de la requête ne peut pas être vide." });

            await _linkPersonneAutoriseeEnfantService.UpdateLinkPersonneAutoriseeEnfantAsync(linkPersonneAutoriseeEnfantDto);

            return NoContent();
        }

        // DELETE: api/v1/linkpersonneautoriseeenfant/personne/{personneId}/enfant/{enfantId}
        [HttpDelete("personne-autorisee/{personneAutoriseeId}/enfant/{enfantId}")]
        public async Task<IActionResult> Delete(int personneAutoriseeId, int enfantId)
        {
            await _linkPersonneAutoriseeEnfantService.RemoveLinkPersonneAutoriseeEnfantAsync(enfantId, personneAutoriseeId);

            return NoContent();
        }
    }
}