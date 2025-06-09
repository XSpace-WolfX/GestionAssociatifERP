using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Models;
using GestionAssociatifERP.Repositories;

namespace GestionAssociatifERP.Services
{
    public class EnfantService : IEnfantService
    {
        private readonly IEnfantRepository _enfantRepository;
        private readonly IMapper _mapper;

        public EnfantService(IEnfantRepository enfantRepository, IMapper mapper)
        {
            _enfantRepository = enfantRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<EnfantDto>>> GetAllEnfantsAsync()
        {
            var enfants = await _enfantRepository.GetAllAsync();
            var enfantsDto = _mapper.Map<IEnumerable<EnfantDto>>(enfants);

            if (enfantsDto == null)
                enfantsDto = new List<EnfantDto>();

            return ServiceResult<IEnumerable<EnfantDto>>.Ok(enfantsDto);
        }

        public async Task<ServiceResult<EnfantDto>> GetEnfantAsync(int id)
        {
            var enfant = await _enfantRepository.GetByIdAsync(id);
            if (enfant == null)
                return ServiceResult<EnfantDto>.Fail("Aucun enfant correspondant n'a été trouvé", ServiceErrorType.NotFound);

            var enfantDto = _mapper.Map<EnfantDto>(enfant);

            return ServiceResult<EnfantDto>.Ok(enfantDto);
        }

        public async Task<ServiceResult<EnfantWithResponsablesDto>> GetEnfantWithResponsablesAsync(int id)
        {
            var enfant = await _enfantRepository.GetWithResponsablesAsync(id);
            if (enfant == null)
                return ServiceResult<EnfantWithResponsablesDto>.Fail("Aucun enfant correspondant n'a été trouvé", ServiceErrorType.NotFound);

            var enfantDto = _mapper.Map<EnfantWithResponsablesDto>(enfant);

            return ServiceResult<EnfantWithResponsablesDto>.Ok(enfantDto);
        }

        public async Task<ServiceResult<EnfantWithPersonnesAutoriseesDto>> GetEnfantWithPersonnesAutoriseesAsync(int id)
        {
            var enfant = await _enfantRepository.GetWithPersonnesAutoriseesAsync(id);
            if (enfant == null)
                return ServiceResult<EnfantWithPersonnesAutoriseesDto>.Fail("Aucun enfant correspondant n'a été trouvé", ServiceErrorType.NotFound);

            var enfantDto = _mapper.Map<EnfantWithPersonnesAutoriseesDto>(enfant);

            return ServiceResult<EnfantWithPersonnesAutoriseesDto>.Ok(enfantDto);
        }

        public async Task<ServiceResult<EnfantWithDonneesSupplementairesDto>> GetEnfantWithDonneesSupplementairesAsync(int id)
        {
            var enfant = await _enfantRepository.GetWithDonneesSupplementairesAsync(id);
            if (enfant == null)
                return ServiceResult<EnfantWithDonneesSupplementairesDto>.Fail("Aucun enfant correspondant n'a été trouvé", ServiceErrorType.NotFound);

            var enfantDto = _mapper.Map<EnfantWithDonneesSupplementairesDto>(enfant);

            return ServiceResult<EnfantWithDonneesSupplementairesDto>.Ok(enfantDto);
        }

        public async Task<ServiceResult<EnfantDto>> CreateEnfantAsync(CreateEnfantDto enfantDto)
        {
            var enfant = _mapper.Map<Enfant>(enfantDto);
            if (enfant == null)
                return ServiceResult<EnfantDto>.Fail("Erreur lors de la création de l'enfant : Le Mapping a échoué", ServiceErrorType.InternalError);

            await _enfantRepository.AddAsync(enfant);

            var createdEnfant = await _enfantRepository.GetByIdAsync(enfant.Id);
            if (createdEnfant == null)
                return ServiceResult<EnfantDto>.Fail("Échec de la création de l'enfant", ServiceErrorType.InternalError);

            var createdEnfantDto = _mapper.Map<EnfantDto>(createdEnfant);

            return ServiceResult<EnfantDto>.Ok(createdEnfantDto);
        }

        public async Task<ServiceResult> UpdateEnfantAsync(int id, UpdateEnfantDto enfantDto)
        {
            if (id != enfantDto.Id)
                return ServiceResult.Fail("L'identifiant de l'enfant ne correspond pas à celui de l'objet envoyé", ServiceErrorType.BadRequest);

            var enfant = await _enfantRepository.GetByIdAsync(id);
            if (enfant == null)
                return ServiceResult.Fail("Aucun enfant correspondant n'a été trouvé", ServiceErrorType.NotFound);

            _mapper.Map(enfantDto, enfant);

            await _enfantRepository.UpdateAsync(enfant);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteEnfantAsync(int id)
        {
            var enfant = await _enfantRepository.GetByIdAsync(id);
            if (enfant == null)
                return ServiceResult.Fail("Aucun enfant correspondant n'a été trouvé", ServiceErrorType.NotFound);

            await _enfantRepository.DeleteAsync(id);

            return ServiceResult.Ok();
        }
    }
}