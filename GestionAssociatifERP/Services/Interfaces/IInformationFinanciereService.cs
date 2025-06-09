using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;

namespace GestionAssociatifERP.Services
{
    public interface IInformationFinanciereService
    {
        Task<ServiceResult<IEnumerable<InformationFinanciereDto>>> GetAllInformationsFinancieresAsync();
        Task<ServiceResult<InformationFinanciereDto>> GetInformationFinanciereAsync(int id);
        Task<ServiceResult<InformationFinanciereDto>> CreateInformationFinanciereAsync(CreateInformationFinanciereDto informationFinanciereDto);
        Task<ServiceResult> UpdateInformationFinanciereAsync(int id, UpdateInformationFinanciereDto informationFinanciereDto);
        Task<ServiceResult> DeleteInformationFinanciereAsync(int id);
    }
}