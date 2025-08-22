using GestionAssociatifERP.Dtos.V1;

namespace GestionAssociatifERP.Services
{
    public interface ILinkPersonneAutoriseeEnfantService
    {
        Task<IEnumerable<LinkPersonneAutoriseeEnfantDto>> GetPersonnesAutoriseesByEnfantIdAsync(int enfantId);
        Task<IEnumerable<LinkPersonneAutoriseeEnfantDto>> GetEnfantsByPersonneAutoriseeIdAsync(int personneAutoriseeId);
        Task<bool> ExistsLinkPersonneAutoriseeEnfantAsync(int enfantId, int personneAutoriseeId);
        Task<LinkPersonneAutoriseeEnfantDto> CreateLinkPersonneAutoriseeEnfantAsync(CreateLinkPersonneAutoriseeEnfantDto personneAutoriseeEnfantDto);
        Task UpdateLinkPersonneAutoriseeEnfantAsync(UpdateLinkPersonneAutoriseeEnfantDto personneAutoriseeEnfantDto);
        Task RemoveLinkPersonneAutoriseeEnfantAsync(int enfantId, int personneAutoriseeId);
    }
}