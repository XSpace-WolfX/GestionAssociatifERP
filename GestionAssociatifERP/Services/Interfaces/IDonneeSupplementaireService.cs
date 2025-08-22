using GestionAssociatifERP.Dtos.V1;

namespace GestionAssociatifERP.Services
{
    public interface IDonneeSupplementaireService
    {
        Task<IEnumerable<DonneeSupplementaireDto>> GetAllDonneesSupplementairesAsync();
        Task<DonneeSupplementaireDto> GetDonneeSupplementaireAsync(int id);
        Task<DonneeSupplementaireDto> CreateDonneeSupplementaireAsync(CreateDonneeSupplementaireDto donneeSupplementaireDto);
        Task UpdateDonneeSupplementaireAsync(int id, UpdateDonneeSupplementaireDto donneeSupplementaireDto);
        Task DeleteDonneeSupplementaireAsync(int id);
    }
}