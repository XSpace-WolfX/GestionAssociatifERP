using GestionAssociatifERP.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionAssociatifERP.Repositories
{
    public class ResponsableEnfantRepository : GenericRepository<ResponsableEnfant>, IResponsableEnfantRepository
    {
        public ResponsableEnfantRepository(GestionAssociatifDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<ResponsableEnfant>> GetResponsablesByEnfantIdAsync(int enfantId)
            => await _dbContext.ResponsableEnfants
                .Include(re => re.Responsable)
                .Where(re => re.EnfantId == enfantId)
                .ToListAsync();

        public async Task<IEnumerable<ResponsableEnfant>> GetEnfantsByResponsableIdAsync(int responsableId)
            => await _dbContext.ResponsableEnfants
                .Include(re => re.Enfant)
                .Where(re => re.ResponsableId == responsableId)
                .ToListAsync();

        public async Task<ResponsableEnfant?> GetLinkAsync(int responsableId, int enfantId)
            => await _dbContext.ResponsableEnfants
                .FirstOrDefaultAsync(re => re.ResponsableId == responsableId && re.EnfantId == enfantId);

        public async Task<bool> LinkExistsAsync(int responsableId, int enfantId)
            => await _dbContext.ResponsableEnfants
                .AnyAsync(re => re.ResponsableId == responsableId && re.EnfantId == enfantId);

        public async Task RemoveLinkAsync(int responsableId, int enfantId)
        {
            var responsableEnfant = await GetLinkAsync(responsableId, enfantId);
            if (responsableEnfant != null)
            {
                _dbContext.ResponsableEnfants.Remove(responsableEnfant);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
