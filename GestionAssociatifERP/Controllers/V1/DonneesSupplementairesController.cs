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
    public class DonneesSupplementairesController : ControllerBase
    {
        private readonly IDonneeSupplementaireService _donneeSupplementaireService;

        public DonneesSupplementairesController(IDonneeSupplementaireService donneeSupplementaireService)
        {
            _donneeSupplementaireService = donneeSupplementaireService;
        }

        // GET: api/v1/donneessupplementaires
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _donneeSupplementaireService.GetAllDonneesSupplementairesAsync();

            return Ok(result.Data);
        }

        // GET: api/v1/donneessupplementaires/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _donneeSupplementaireService.GetDonneeSupplementaireAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // POST: api/v1/donneessupplementaires
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonneeSupplementaireDto donneeSupplementaireDto)
        {
            if (donneeSupplementaireDto == null)
                return BadRequest(new { Message = "Le corps de la requête ne peut pas être vide." });

            var result = await _donneeSupplementaireService.CreateDonneeSupplementaireAsync(donneeSupplementaireDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.InternalError)
                return StatusCode(500, new 
                {
                    MessageContent = result.Message
                });

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/v1/donneessupplementaires/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDonneeSupplementaireDto donneeSupplementaireDto)
        {
            var result = await _donneeSupplementaireService.UpdateDonneeSupplementaireAsync(id, donneeSupplementaireDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.BadRequest)
                return BadRequest(new { result.Message });
            else if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }

        // DELETE: api/v1/donneessupplementaires/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donneeSupplementaireService.DeleteDonneeSupplementaireAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }
    }
}