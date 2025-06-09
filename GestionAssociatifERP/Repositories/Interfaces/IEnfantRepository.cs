using GestionAssociatifERP.Models;

namespace GestionAssociatifERP.Repositories
{
    public interface IEnfantRepository : IGenericRepository<Enfant>
    {
        Task<Enfant?> GetWithDonneesSupplementairesAsync(int id);
        Task<Enfant?> GetWithPersonnesAutoriseesAsync(int id);
        Task<Enfant?> GetWithResponsablesAsync(int id);
    }
}
