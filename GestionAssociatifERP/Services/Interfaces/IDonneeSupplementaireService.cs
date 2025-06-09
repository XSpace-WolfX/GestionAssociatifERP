using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;

namespace GestionAssociatifERP.Services
{
    public interface IDonneeSupplementaireService
    {
        Task<ServiceResult<IEnumerable<DonneeSupplementaireDto>>> GetAllDonneesSupplementairesAsync();
        Task<ServiceResult<DonneeSupplementaireDto>> GetDonneeSupplementaireAsync(int id);
        Task<ServiceResult<DonneeSupplementaireDto>> CreateDonneeSupplementaireAsync(CreateDonneeSupplementaireDto donneeSupplementaireDto);
        Task<ServiceResult> UpdateDonneeSupplementaireAsync(int id, UpdateDonneeSupplementaireDto donneeSupplementaireDto);
        Task<ServiceResult> DeleteDonneeSupplementaireAsync(int id);
    }
}