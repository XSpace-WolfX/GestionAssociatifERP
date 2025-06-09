using GestionAssociatifERP.Models;

namespace GestionAssociatifERP.Repositories
{
    public class InformationFinanciereRepository : GenericRepository<InformationFinanciere>, IInformationFinanciereRepository
    {
        public InformationFinanciereRepository(GestionAssociatifDbContext dbContext) : base(dbContext) { }
    }
}