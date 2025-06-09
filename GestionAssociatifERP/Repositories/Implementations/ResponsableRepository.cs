using GestionAssociatifERP.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionAssociatifERP.Repositories
{
    public class ResponsableRepository : GenericRepository<Responsable>, IResponsableRepository
    {
        public ResponsableRepository(GestionAssociatifDbContext dbContext) : base(dbContext) { }

        public async Task<Responsable?> GetWithInformationFinanciereAsync(int id)
        {
            return await _dbContext.Responsables
                .Include(r => r.InformationFinanciere)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Responsable?> GetWithSituationPersonnelleAsync(int id)
        {
            return await _dbContext.Responsables
                .Include(r => r.SituationPersonnelle)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Responsable?> GetWithEnfantsAsync(int id)
        {
            return await _dbContext.Responsables
                .Include(r => r.ResponsableEnfants)
                    .ThenInclude(re => re.Enfant)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
