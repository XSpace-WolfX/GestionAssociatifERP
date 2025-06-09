using GestionAssociatifERP.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionAssociatifERP.Repositories
{
    public class PersonneAutoriseeRepository : GenericRepository<PersonneAutorisee>, IPersonneAutoriseeRepository
    {
        public PersonneAutoriseeRepository(GestionAssociatifDbContext dbContext) : base(dbContext) { }

        public async Task<PersonneAutorisee?> GetWithEnfantsAsync(int id)
        {
            return await _dbContext.PersonneAutorisees
                .Include(p => p.PersonneAutoriseeEnfants)
                    .ThenInclude(pae => pae.Enfant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}