using GestionAssociatifERP.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionAssociatifERP.Repositories
{
    public class PersonneAutoriseeEnfantRepository : GenericRepository<PersonneAutoriseeEnfant>, IPersonneAutoriseeEnfantRepository
    {
        public PersonneAutoriseeEnfantRepository(GestionAssociatifDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<PersonneAutoriseeEnfant>> GetPersonnesAutoriseesByEnfantIdAsync(int enfantId)
            => await _dbContext.PersonneAutoriseeEnfants
                .Include(pae => pae.PersonneAutorisee)
                .Where(pae => pae.EnfantId == enfantId)
                .ToListAsync();

        public async Task<IEnumerable<PersonneAutoriseeEnfant>> GetEnfantsByPersonneAutoriseeIdAsync(int personneAutoriseeId)
            => await _dbContext.PersonneAutoriseeEnfants
                .Include(pae => pae.Enfant)
                .Where(pae => pae.PersonneAutoriseeId == personneAutoriseeId)
                .ToListAsync();

        public async Task<PersonneAutoriseeEnfant?> GetLinkAsync(int personneAutoriseeId, int enfantId)
            => await _dbContext.PersonneAutoriseeEnfants
                .FirstOrDefaultAsync(personneAutoriseeEnfant => personneAutoriseeEnfant.PersonneAutoriseeId == personneAutoriseeId && personneAutoriseeEnfant.EnfantId == enfantId);

        public async Task<bool> LinkExistsAsync(int personneAutoriseeId, int enfantId)
            => await _dbContext.PersonneAutoriseeEnfants
                .AnyAsync(pae => pae.PersonneAutoriseeId == personneAutoriseeId && pae.EnfantId == enfantId);

        public async Task RemoveLinkAsync(int personneAutoriseeId, int enfantId)
        {
            var personneAutoriseeEnfant = await GetLinkAsync(personneAutoriseeId, enfantId);
            if (personneAutoriseeEnfant != null)
            {
                _dbContext.PersonneAutoriseeEnfants.Remove(personneAutoriseeEnfant);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
