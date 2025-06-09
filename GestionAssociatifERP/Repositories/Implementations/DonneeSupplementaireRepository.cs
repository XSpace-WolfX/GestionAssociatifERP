using GestionAssociatifERP.Models;

namespace GestionAssociatifERP.Repositories
{
    public class DonneeSupplementaireRepository : GenericRepository<DonneeSupplementaire>, IDonneeSupplementaireRepository
    {
        public DonneeSupplementaireRepository(GestionAssociatifDbContext dbContext) : base(dbContext) { }
    }
}