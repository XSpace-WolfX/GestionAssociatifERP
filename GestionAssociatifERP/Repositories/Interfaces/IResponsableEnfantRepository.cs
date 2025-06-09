using GestionAssociatifERP.Models;

namespace GestionAssociatifERP.Repositories
{
    public interface IResponsableEnfantRepository : IGenericRepository<ResponsableEnfant>
    {
        Task<IEnumerable<ResponsableEnfant>> GetResponsablesByEnfantIdAsync(int enfantId);
        Task<IEnumerable<ResponsableEnfant>> GetEnfantsByResponsableIdAsync(int responsableId);
        Task<ResponsableEnfant?> GetLinkAsync(int responsableId, int enfantId);
        Task<bool> LinkExistsAsync(int responsableId, int enfantId);
        Task RemoveLinkAsync(int responsableId, int enfantId);
    }
}