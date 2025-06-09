using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;

namespace GestionAssociatifERP.Services
{
    public interface ILinkResponsableEnfantService
    {
        Task<ServiceResult<IEnumerable<LinkResponsableEnfantDto>>> GetResponsablesByEnfantIdAsync(int enfantId);
        Task<ServiceResult<IEnumerable<LinkResponsableEnfantDto>>> GetEnfantsByResponsableIdAsync(int responsableId);
        Task<ServiceResult<bool>> ExistsLinkResponsableEnfantAsync(int enfantId, int responsableId);
        Task<ServiceResult<LinkResponsableEnfantDto>> CreateLinkResponsableEnfantAsync(CreateLinkResponsableEnfantDto responsableEnfantDto);
        Task<ServiceResult> UpdateLinkResponsableEnfantAsync(UpdateLinkResponsableEnfantDto responsableEnfantDto);
        Task<ServiceResult> RemoveLinkResponsableEnfantAsync(int enfantId, int responsableId);
    }
}