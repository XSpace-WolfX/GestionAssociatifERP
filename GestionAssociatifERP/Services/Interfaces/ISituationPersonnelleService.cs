using GestionAssociatifERP.Dtos.V1;

namespace GestionAssociatifERP.Services
{
    public interface ISituationPersonnelleService
    {
        Task<IEnumerable<SituationPersonnelleDto>> GetAllSituationsPersonnellesAsync();
        Task<SituationPersonnelleDto> GetSituationPersonnelleAsync(int id);
        Task<SituationPersonnelleDto> CreateSituationPersonnelleAsync(CreateSituationPersonnelleDto situationPersonnelleDto);
        Task UpdateSituationPersonnelleAsync(int id, UpdateSituationPersonnelleDto situationPersonnelleDto);
        Task DeleteSituationPersonnelleAsync(int id);
    }
}