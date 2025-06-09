using GestionAssociatifERP.Models;

namespace GestionAssociatifERP.Repositories
{
    public interface IPersonneAutoriseeRepository : IGenericRepository<PersonneAutorisee>
    {
        Task<PersonneAutorisee?> GetWithEnfantsAsync(int id);
    }
}
