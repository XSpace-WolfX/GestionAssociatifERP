using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;

namespace GestionAssociatifERP.Services
{
    public interface ISituationPersonnelleService
    {
        Task<ServiceResult<IEnumerable<SituationPersonnelleDto>>> GetAllSituationsPersonnellesAsync();
        Task<ServiceResult<SituationPersonnelleDto>> GetSituationPersonnelleAsync(int id);
        Task<ServiceResult<SituationPersonnelleDto>> CreateSituationPersonnelleAsync(CreateSituationPersonnelleDto situationPersonnelleDto);
        Task<ServiceResult> UpdateSituationPersonnelleAsync(int id, UpdateSituationPersonnelleDto situationPersonnelleDto);
        Task<ServiceResult> DeleteSituationPersonnelleAsync(int id);
    }
}