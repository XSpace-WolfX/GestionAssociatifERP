using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Models;
using GestionAssociatifERP.Repositories;

namespace GestionAssociatifERP.Services
{
    public class PersonneAutoriseeService : IPersonneAutoriseeService
    {
        private readonly IPersonneAutoriseeRepository _personneAutoriseeRepository;
        private readonly IMapper _mapper;

        public PersonneAutoriseeService(IPersonneAutoriseeRepository personneAutoriseeRepository, IMapper mapper)
        {
            _personneAutoriseeRepository = personneAutoriseeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<PersonneAutoriseeDto>>> GetAllPersonnesAutoriseesAsync()
        {
            var personnesAutorisees = await _personneAutoriseeRepository.GetAllAsync();
            var personnesAutoriseesDto = _mapper.Map<IEnumerable<PersonneAutoriseeDto>>(personnesAutorisees);

            if (personnesAutoriseesDto == null)
                personnesAutoriseesDto = new List<PersonneAutoriseeDto>();

            return ServiceResult<IEnumerable<PersonneAutoriseeDto>>.Ok(personnesAutoriseesDto);
        }

        public async Task<ServiceResult<PersonneAutoriseeDto>> GetPersonneAutoriseeAsync(int id)
        {
            var personneAutorisee = await _personneAutoriseeRepository.GetByIdAsync(id);
            if (personneAutorisee == null)
                return ServiceResult<PersonneAutoriseeDto>.Fail("Aucune personne autorisée correspondante n'a été trouvée", ServiceErrorType.NotFound);

            var personneAutoriseeDto = _mapper.Map<PersonneAutoriseeDto>(personneAutorisee);

            return ServiceResult<PersonneAutoriseeDto>.Ok(personneAutoriseeDto);
        }

        public async Task<ServiceResult<PersonneAutoriseeWithEnfantsDto>> GetPersonneAutoriseeWithEnfantsAsync(int id)
        {
            var personneAutorisee = await _personneAutoriseeRepository.GetWithEnfantsAsync(id);
            if (personneAutorisee == null)
                return ServiceResult<PersonneAutoriseeWithEnfantsDto>.Fail("Aucune personne autorisée correspondante n'a été trouvée", ServiceErrorType.NotFound);

            var personneAutoriseeDto = _mapper.Map<PersonneAutoriseeWithEnfantsDto>(personneAutorisee);

            return ServiceResult<PersonneAutoriseeWithEnfantsDto>.Ok(personneAutoriseeDto);
        }

        public async Task<ServiceResult<PersonneAutoriseeDto>> CreatePersonneAutoriseeAsync(CreatePersonneAutoriseeDto personneAutoriseeDto)
        {
            var personneAutorisee = _mapper.Map<PersonneAutorisee>(personneAutoriseeDto);
            if (personneAutorisee == null)
                return ServiceResult<PersonneAutoriseeDto>.Fail("Erreur lors de la création de la personne autorisée : Le Mapping a échoué", ServiceErrorType.InternalError);

            await _personneAutoriseeRepository.AddAsync(personneAutorisee);

            var createdPersonneAutorisee = await _personneAutoriseeRepository.GetByIdAsync(personneAutorisee.Id);
            if (createdPersonneAutorisee == null)
                return ServiceResult<PersonneAutoriseeDto>.Fail("Échec de la création de la personne autorisée", ServiceErrorType.InternalError);

            var createdPersonneAutoriseeDto = _mapper.Map<PersonneAutoriseeDto>(personneAutorisee);

            return ServiceResult<PersonneAutoriseeDto>.Ok(createdPersonneAutoriseeDto);
        }

        public async Task<ServiceResult> UpdatePersonneAutoriseeAsync(int id, UpdatePersonneAutoriseeDto personneAutoriseeDto)
        {
            if (id != personneAutoriseeDto.Id)
                return ServiceResult.Fail("L'identifiant de la personne autorisée ne correspond pas à celui de l'objet envoyé", ServiceErrorType.BadRequest);

            var personneAutorisee = await _personneAutoriseeRepository.GetByIdAsync(id);
            if (personneAutorisee is null)
                return ServiceResult.Fail("Aucune personne autorisée correspondante n'a été trouvée", ServiceErrorType.NotFound);

            _mapper.Map(personneAutoriseeDto, personneAutorisee);

            await _personneAutoriseeRepository.UpdateAsync(personneAutorisee);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeletePersonneAutoriseeAsync(int id)
        {
            var personneAutorisee = await _personneAutoriseeRepository.GetByIdAsync(id);
            if (personneAutorisee is null)
                return ServiceResult.Fail("Aucune personne autorisée correspondante n'a été trouvée", ServiceErrorType.NotFound);

            await _personneAutoriseeRepository.DeleteAsync(id);

            return ServiceResult.Ok();
        }
    }
}