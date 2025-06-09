using GestionAssociatifERP.Models;

namespace GestionAssociatifERP.Repositories
{
    public interface IPersonneAutoriseeEnfantRepository : IGenericRepository<PersonneAutoriseeEnfant>
    {
        Task<IEnumerable<PersonneAutoriseeEnfant>> GetPersonnesAutoriseesByEnfantIdAsync(int enfantId);
        Task<IEnumerable<PersonneAutoriseeEnfant>> GetEnfantsByPersonneAutoriseeIdAsync(int personneAutoriseeId);
        Task<PersonneAutoriseeEnfant?> GetLinkAsync(int personneAutoriseeId, int enfantId);
        Task<bool> LinkExistsAsync(int personneAutoriseeId, int enfantId);
        Task RemoveLinkAsync(int personneAutoriseeId, int enfantId);
    }
}