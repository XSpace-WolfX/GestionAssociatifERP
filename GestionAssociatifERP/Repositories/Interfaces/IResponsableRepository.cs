using GestionAssociatifERP.Models;

namespace GestionAssociatifERP.Repositories
{
    public interface IResponsableRepository : IGenericRepository<Responsable>
    {
        public Task<Responsable?> GetWithInformationFinanciereAsync(int id);
        public Task<Responsable?> GetWithSituationPersonnelleAsync(int id);
        public Task<Responsable?> GetWithEnfantsAsync(int id);
    }
}
