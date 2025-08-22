using GestionAssociatifERP.Dtos.V1;

namespace GestionAssociatifERP.Services
{
    public interface IResponsableService
    {
        Task<IEnumerable<ResponsableDto>> GetAllResponsablesAsync();
        Task<ResponsableDto> GetResponsableAsync(int id);
        Task<ResponsableWithInformationFinanciereDto> GetResponsableWithInformationFinanciereAsync(int id);
        Task<ResponsableWithSituationPersonnelleDto> GetResponsableWithSituationPersonnelleAsync(int id);
        Task<ResponsableWithEnfantsDto> GetResponsableWithEnfantsAsync(int id);
        Task<ResponsableDto> CreateResponsableAsync(CreateResponsableDto responsableDto);
        Task UpdateResponsableAsync(int id, UpdateResponsableDto responsableDto);
        Task DeleteResponsableAsync(int id);
    }
}