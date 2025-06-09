using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;

namespace GestionAssociatifERP.Services
{
    public interface IEnfantService
    {
        Task<ServiceResult<IEnumerable<EnfantDto>>> GetAllEnfantsAsync();
        Task<ServiceResult<EnfantDto>> GetEnfantAsync(int id);
        Task<ServiceResult<EnfantWithResponsablesDto>> GetEnfantWithResponsablesAsync(int id);
        Task<ServiceResult<EnfantWithPersonnesAutoriseesDto>> GetEnfantWithPersonnesAutoriseesAsync(int id);
        Task<ServiceResult<EnfantWithDonneesSupplementairesDto>> GetEnfantWithDonneesSupplementairesAsync(int id);
        Task<ServiceResult<EnfantDto>> CreateEnfantAsync(CreateEnfantDto enfantDto);
        Task<ServiceResult> UpdateEnfantAsync(int id, UpdateEnfantDto enfantDto);
        Task<ServiceResult> DeleteEnfantAsync(int id);
    }
}
