using GestionAssociatifERP.Models;

namespace GestionAssociatifERP.Repositories
{
    public class SituationPersonnelleRepository : GenericRepository<SituationPersonnelle>, ISituationPersonnelleRepository
    {
        public SituationPersonnelleRepository(GestionAssociatifDbContext dbContext) : base(dbContext) { }
    }
}