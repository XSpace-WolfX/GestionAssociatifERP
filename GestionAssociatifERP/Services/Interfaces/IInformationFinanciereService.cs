using GestionAssociatifERP.Dtos.V1;

namespace GestionAssociatifERP.Services
{
    public interface IInformationFinanciereService
    {
        Task<IEnumerable<InformationFinanciereDto>> GetAllInformationsFinancieresAsync();
        Task<InformationFinanciereDto> GetInformationFinanciereAsync(int id);
        Task<InformationFinanciereDto> CreateInformationFinanciereAsync(CreateInformationFinanciereDto informationFinanciereDto);
        Task UpdateInformationFinanciereAsync(int id, UpdateInformationFinanciereDto informationFinanciereDto);
        Task DeleteInformationFinanciereAsync(int id);
    }
}