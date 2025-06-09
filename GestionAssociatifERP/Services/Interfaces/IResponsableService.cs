using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;

namespace GestionAssociatifERP.Services
{
    public interface IResponsableService
    {
        Task<ServiceResult<IEnumerable<ResponsableDto>>> GetAllResponsablesAsync();
        Task<ServiceResult<ResponsableDto>> GetResponsableAsync(int id);
        Task<ServiceResult<ResponsableWithInformationFinanciereDto>> GetResponsableWithInformationFinanciereAsync(int id);
        Task<ServiceResult<ResponsableWithSituationPersonnelleDto>> GetResponsableWithSituationPersonnelleAsync(int id);
        Task<ServiceResult<ResponsableWithEnfantsDto>> GetResponsableWithEnfantsAsync(int id);
        Task<ServiceResult<ResponsableDto>> CreateResponsableAsync(CreateResponsableDto responsableDto);
        Task<ServiceResult> UpdateResponsableAsync(int id, UpdateResponsableDto responsableDto);
        Task<ServiceResult> DeleteResponsableAsync(int id);
    }
}
