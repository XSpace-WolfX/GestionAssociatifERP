using GestionAssociatifERP.Dtos.V1;

namespace GestionAssociatifERP.Services
{
    public interface IEnfantService
    {
        Task<IEnumerable<EnfantDto>> GetAllEnfantsAsync();
        Task<EnfantDto> GetEnfantAsync(int id);
        Task<EnfantWithResponsablesDto> GetEnfantWithResponsablesAsync(int id);
        Task<EnfantWithPersonnesAutoriseesDto> GetEnfantWithPersonnesAutoriseesAsync(int id);
        Task<EnfantWithDonneesSupplementairesDto> GetEnfantWithDonneesSupplementairesAsync(int id);
        Task<EnfantDto> CreateEnfantAsync(CreateEnfantDto enfantDto);
        Task UpdateEnfantAsync(int id, UpdateEnfantDto enfantDto);
        Task DeleteEnfantAsync(int id);
    }
}