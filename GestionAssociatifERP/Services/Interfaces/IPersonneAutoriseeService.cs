using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;

namespace GestionAssociatifERP.Services
{
    public interface IPersonneAutoriseeService
    {
        Task<ServiceResult<IEnumerable<PersonneAutoriseeDto>>> GetAllPersonnesAutoriseesAsync();
        Task<ServiceResult<PersonneAutoriseeDto>> GetPersonneAutoriseeAsync(int id);
        Task<ServiceResult<PersonneAutoriseeWithEnfantsDto>> GetPersonneAutoriseeWithEnfantsAsync(int id);
        Task<ServiceResult<PersonneAutoriseeDto>> CreatePersonneAutoriseeAsync(CreatePersonneAutoriseeDto personneAutoriseeDto);
        Task<ServiceResult> UpdatePersonneAutoriseeAsync(int id, UpdatePersonneAutoriseeDto personneAutoriseeDto);
        Task<ServiceResult> DeletePersonneAutoriseeAsync(int id);
    }
}