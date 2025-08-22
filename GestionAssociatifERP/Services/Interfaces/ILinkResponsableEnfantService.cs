using GestionAssociatifERP.Dtos.V1;

namespace GestionAssociatifERP.Services
{
    public interface ILinkResponsableEnfantService
    {
        Task<IEnumerable<LinkResponsableEnfantDto>> GetResponsablesByEnfantIdAsync(int enfantId);
        Task<IEnumerable<LinkResponsableEnfantDto>> GetEnfantsByResponsableIdAsync(int responsableId);
        Task<bool> ExistsLinkResponsableEnfantAsync(int enfantId, int responsableId);
        Task<LinkResponsableEnfantDto> CreateLinkResponsableEnfantAsync(CreateLinkResponsableEnfantDto responsableEnfantDto);
        Task UpdateLinkResponsableEnfantAsync(UpdateLinkResponsableEnfantDto responsableEnfantDto);
        Task RemoveLinkResponsableEnfantAsync(int enfantId, int responsableId);
    }
}