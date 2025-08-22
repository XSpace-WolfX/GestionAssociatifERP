using GestionAssociatifERP.Dtos.V1;

namespace GestionAssociatifERP.Services
{
    public interface IPersonneAutoriseeService
    {
        Task<IEnumerable<PersonneAutoriseeDto>> GetAllPersonnesAutoriseesAsync();
        Task<PersonneAutoriseeDto> GetPersonneAutoriseeAsync(int id);
        Task<PersonneAutoriseeWithEnfantsDto> GetPersonneAutoriseeWithEnfantsAsync(int id);
        Task<PersonneAutoriseeDto> CreatePersonneAutoriseeAsync(CreatePersonneAutoriseeDto personneAutoriseeDto);
        Task UpdatePersonneAutoriseeAsync(int id, UpdatePersonneAutoriseeDto personneAutoriseeDto);
        Task DeletePersonneAutoriseeAsync(int id);
    }
}