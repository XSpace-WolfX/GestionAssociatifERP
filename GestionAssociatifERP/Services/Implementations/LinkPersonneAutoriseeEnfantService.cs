using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Models;
using GestionAssociatifERP.Repositories;

namespace GestionAssociatifERP.Services
{
    public class LinkPersonneAutoriseeEnfantService : ILinkPersonneAutoriseeEnfantService
    {
        private readonly IPersonneAutoriseeEnfantRepository _personneAutoriseeEnfantRepository;
        private readonly IEnfantRepository _enfantRepository;
        private readonly IPersonneAutoriseeRepository _personneAutoriseeRepository;
        private readonly IMapper _mapper;

        public LinkPersonneAutoriseeEnfantService(IPersonneAutoriseeEnfantRepository personneAutoriseeEnfantRepository, IEnfantRepository enfantRepository, IPersonneAutoriseeRepository personneAutoriseeRepository, IMapper mapper)
        {
            _personneAutoriseeEnfantRepository = personneAutoriseeEnfantRepository;
            _enfantRepository = enfantRepository;
            _personneAutoriseeRepository = personneAutoriseeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<LinkPersonneAutoriseeEnfantDto>>> GetPersonnesAutoriseesByEnfantIdAsync(int enfantId)
        {
            var exists = await _enfantRepository.ExistsAsync(e => e.Id == enfantId);
            if (!exists)
                return ServiceResult<IEnumerable<LinkPersonneAutoriseeEnfantDto>>.Fail("L'enfant spécifié n'existe pas", ServiceErrorType.NotFound);

            var linkPersonneAutoriseeEnfant = await _personneAutoriseeEnfantRepository.GetPersonnesAutoriseesByEnfantIdAsync(enfantId);

            var linkPersonneAutoriseeEnfantDto = _mapper.Map<IEnumerable<LinkPersonneAutoriseeEnfantDto>>(linkPersonneAutoriseeEnfant);

            return ServiceResult<IEnumerable<LinkPersonneAutoriseeEnfantDto>>.Ok(linkPersonneAutoriseeEnfantDto);
        }

        public async Task<ServiceResult<IEnumerable<LinkPersonneAutoriseeEnfantDto>>> GetEnfantsByPersonneAutoriseeIdAsync(int personneAutoriseeId)
        {
            var exists = await _personneAutoriseeRepository.ExistsAsync(pa => pa.Id == personneAutoriseeId);
            if (!exists)
                return ServiceResult<IEnumerable<LinkPersonneAutoriseeEnfantDto>>.Fail("La personne autorisée spécifiée n'existe pas", ServiceErrorType.NotFound);

            var linkPersonneAutoriseeEnfant = await _personneAutoriseeEnfantRepository.GetEnfantsByPersonneAutoriseeIdAsync(personneAutoriseeId);

            var linkPersonneAutoriseeEnfantDto = _mapper.Map<IEnumerable<LinkPersonneAutoriseeEnfantDto>>(linkPersonneAutoriseeEnfant);

            return ServiceResult<IEnumerable<LinkPersonneAutoriseeEnfantDto>>.Ok(linkPersonneAutoriseeEnfantDto);
        }

        public async Task<ServiceResult<bool>> ExistsLinkPersonneAutoriseeEnfantAsync(int enfantId, int personneAutoriseeId)
        {
            var exists = await _personneAutoriseeEnfantRepository.LinkExistsAsync(personneAutoriseeId, enfantId);

            return ServiceResult<bool>.Ok(exists);
        }

        public async Task<ServiceResult<LinkPersonneAutoriseeEnfantDto>> CreateLinkPersonneAutoriseeEnfantAsync(CreateLinkPersonneAutoriseeEnfantDto personneAutoriseeEnfantDto)
        {
            if (!await _personneAutoriseeRepository.ExistsAsync(pa => pa.Id == personneAutoriseeEnfantDto.PersonneAutoriseeId))
                return ServiceResult<LinkPersonneAutoriseeEnfantDto>.Fail("La personne autorisée spécifiée n'existe pas", ServiceErrorType.NotFound);
            else if (!await _enfantRepository.ExistsAsync(e => e.Id == personneAutoriseeEnfantDto.EnfantId))
                return ServiceResult<LinkPersonneAutoriseeEnfantDto>.Fail("L'enfant spécifié n'existe pas", ServiceErrorType.NotFound);

            if (await _personneAutoriseeEnfantRepository.LinkExistsAsync(personneAutoriseeEnfantDto.PersonneAutoriseeId, personneAutoriseeEnfantDto.EnfantId))
                return ServiceResult<LinkPersonneAutoriseeEnfantDto>.Fail("Ce lien existe déjà entre cette personne autorisée et cet enfant", ServiceErrorType.Conflict);

            var personneAutoriseeEnfant = _mapper.Map<PersonneAutoriseeEnfant>(personneAutoriseeEnfantDto);
            if (personneAutoriseeEnfant == null)
                return ServiceResult<LinkPersonneAutoriseeEnfantDto>.Fail("Erreur lors de la création du lien Personne Autorisée / Enfant : Le Mapping a échoué", ServiceErrorType.InternalError);

            await _personneAutoriseeEnfantRepository.AddAsync(personneAutoriseeEnfant);

            var createdPersonneAutoriseeEnfant = await _personneAutoriseeEnfantRepository.GetLinkAsync(personneAutoriseeEnfant.PersonneAutoriseeId, personneAutoriseeEnfant.EnfantId);
            if (createdPersonneAutoriseeEnfant == null)
                return ServiceResult<LinkPersonneAutoriseeEnfantDto>.Fail("Échec de la création du lien Personne Autorisée / Enfant", ServiceErrorType.InternalError);

            var createdPersonneAutoriseeEnfantDto = _mapper.Map<LinkPersonneAutoriseeEnfantDto>(createdPersonneAutoriseeEnfant);

            return ServiceResult<LinkPersonneAutoriseeEnfantDto>.Ok(createdPersonneAutoriseeEnfantDto);
        }

        public async Task<ServiceResult> UpdateLinkPersonneAutoriseeEnfantAsync(UpdateLinkPersonneAutoriseeEnfantDto personneAutoriseeEnfantDto)
        {
            if (personneAutoriseeEnfantDto == null) // TODO dans le controller
                return ServiceResult.Fail("Le lien envoyé est vide");

            var personneAutoriseeEnfant = await _personneAutoriseeEnfantRepository.GetLinkAsync(personneAutoriseeEnfantDto.PersonneAutoriseeId, personneAutoriseeEnfantDto.EnfantId);
            if (personneAutoriseeEnfant == null)
                return ServiceResult.Fail("Le lien Personne Autorisée / Enfant n'existe pas", ServiceErrorType.NotFound);

            _mapper.Map(personneAutoriseeEnfantDto, personneAutoriseeEnfant);

            await _personneAutoriseeEnfantRepository.UpdateAsync(personneAutoriseeEnfant);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> RemoveLinkPersonneAutoriseeEnfantAsync(int enfantId, int personneAutoriseeId)
        {
            if (!await _personneAutoriseeEnfantRepository.LinkExistsAsync(personneAutoriseeId, enfantId))
                return ServiceResult.Fail("Le lien Personne Autorisée / Enfant n'existe pas", ServiceErrorType.NotFound);

            await _personneAutoriseeEnfantRepository.RemoveLinkAsync(personneAutoriseeId, enfantId);

            return ServiceResult.Ok();
        }
    }
}