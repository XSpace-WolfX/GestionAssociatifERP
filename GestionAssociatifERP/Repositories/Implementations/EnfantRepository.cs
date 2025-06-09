using GestionAssociatifERP.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionAssociatifERP.Repositories
{
    public class EnfantRepository : GenericRepository<Enfant>, IEnfantRepository
    {
        public EnfantRepository(GestionAssociatifDbContext dbContext) : base(dbContext) { }

        public async Task<Enfant?> GetWithDonneesSupplementairesAsync(int id)
        {
            return await _dbContext.Enfants
                .Include(e => e.DonneeSupplementaires)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Enfant?> GetWithPersonnesAutoriseesAsync(int id)
        {
            return await _dbContext.Enfants
                .Include(e => e.PersonneAutoriseeEnfants)
                    .ThenInclude(pae => pae.PersonneAutorisee)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Enfant?> GetWithResponsablesAsync(int id)
        {
            return await _dbContext.Enfants
                .Include(e => e.ResponsableEnfants)
                    .ThenInclude(re => re.Responsable)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
