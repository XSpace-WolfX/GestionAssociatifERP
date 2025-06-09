using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;

namespace GestionAssociatifERP.Services
{
    public interface ILinkPersonneAutoriseeEnfantService
    {
        Task<ServiceResult<IEnumerable<LinkPersonneAutoriseeEnfantDto>>> GetPersonnesAutoriseesByEnfantIdAsync(int enfantId);
        Task<ServiceResult<IEnumerable<LinkPersonneAutoriseeEnfantDto>>> GetEnfantsByPersonneAutoriseeIdAsync(int personneAutoriseeId);
        Task<ServiceResult<bool>> ExistsLinkPersonneAutoriseeEnfantAsync(int enfantId, int personneAutoriseeId);
        Task<ServiceResult<LinkPersonneAutoriseeEnfantDto>> CreateLinkPersonneAutoriseeEnfantAsync(CreateLinkPersonneAutoriseeEnfantDto personneAutoriseeEnfantDto);
        Task<ServiceResult> UpdateLinkPersonneAutoriseeEnfantAsync(UpdateLinkPersonneAutoriseeEnfantDto personneAutoriseeEnfantDto);
        Task<ServiceResult> RemoveLinkPersonneAutoriseeEnfantAsync(int enfantId, int personneAutoriseeId);
    }
}