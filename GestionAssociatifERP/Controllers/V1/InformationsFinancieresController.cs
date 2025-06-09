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
    public class InformationsFinancieresController : ControllerBase
    {
        public readonly IInformationFinanciereService _informationFinanciereService;

        public InformationsFinancieresController(IInformationFinanciereService informationFinanciereService)
        {
            _informationFinanciereService = informationFinanciereService;
        }

        // GET: api/v1/informationsfinancieres
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _informationFinanciereService.GetAllInformationsFinancieresAsync();

            return Ok(result.Data);
        }

        // GET: api/v1/informationsfinancieres/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _informationFinanciereService.GetInformationFinanciereAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // POST: api/v1/informationsfinancieres
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInformationFinanciereDto informationFinanciereDto)
        {
            if (informationFinanciereDto == null)
                return BadRequest(new { Message = "Le corps de la requête ne peut pas être vide." });

            var result = await _informationFinanciereService.CreateInformationFinanciereAsync(informationFinanciereDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.InternalError)
                return StatusCode(500, new
                {
                    MessageContent = result.Message
                });

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/v1/informationsfinancieres/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInformationFinanciereDto informationFinanciereDto)
        {
            var result = await _informationFinanciereService.UpdateInformationFinanciereAsync(id, informationFinanciereDto);
            if (!result.Success && result.ErrorType == ServiceErrorType.BadRequest)
                return BadRequest(new { result.Message });
            else if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }

        // DELETE: api/v1/informationsfinancieres/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _informationFinanciereService.DeleteInformationFinanciereAsync(id);
            if (!result.Success && result.ErrorType == ServiceErrorType.NotFound)
                return NotFound(new { result.Message });

            return NoContent();
        }
    }
}