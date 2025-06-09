using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Models;
using GestionAssociatifERP.Repositories;

namespace GestionAssociatifERP.Services
{
    public class SituationPersonnelleService : ISituationPersonnelleService
    {
        private readonly ISituationPersonnelleRepository _situationPersonnelleRepository;
        private readonly IMapper _mapper;

        public SituationPersonnelleService(ISituationPersonnelleRepository situationPersonnelleRepository, IMapper mapper)
        {
            _situationPersonnelleRepository = situationPersonnelleRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<SituationPersonnelleDto>>> GetAllSituationsPersonnellesAsync()
        {
            var situationsPersonnelles = await _situationPersonnelleRepository.GetAllAsync();
            var situationsPersonnellesDto = _mapper.Map<IEnumerable<SituationPersonnelleDto>>(situationsPersonnelles);

            if (situationsPersonnellesDto == null)
                situationsPersonnellesDto = new List<SituationPersonnelleDto>();

            return ServiceResult<IEnumerable<SituationPersonnelleDto>>.Ok(situationsPersonnellesDto);
        }

        public async Task<ServiceResult<SituationPersonnelleDto>> GetSituationPersonnelleAsync(int id)
        {
            var situationPersonnelle = await _situationPersonnelleRepository.GetByIdAsync(id);
            if (situationPersonnelle == null)
                return ServiceResult<SituationPersonnelleDto>.Fail("Aucune situation personnelle correspondante n'a été trouvée", ServiceErrorType.NotFound);

            var situationPersonnelleDto = _mapper.Map<SituationPersonnelleDto>(situationPersonnelle);

            return ServiceResult<SituationPersonnelleDto>.Ok(situationPersonnelleDto);
        }

        public async Task<ServiceResult<SituationPersonnelleDto>> CreateSituationPersonnelleAsync(CreateSituationPersonnelleDto situationPersonnelleDto)
        {
            var situationPersonnelle = _mapper.Map<SituationPersonnelle>(situationPersonnelleDto);
            if (situationPersonnelle == null)
                return ServiceResult<SituationPersonnelleDto>.Fail("Erreur lors de la création de la situation personnelle : Le Mapping a échoué", ServiceErrorType.InternalError);

            await _situationPersonnelleRepository.AddAsync(situationPersonnelle);

            var situationPersonnelleCreated = await _situationPersonnelleRepository.GetByIdAsync(situationPersonnelle.Id);
            if (situationPersonnelleCreated == null)
                return ServiceResult<SituationPersonnelleDto>.Fail("Échec de la création de la situation personnelle", ServiceErrorType.InternalError);

            var situationPersonnelleDtoCreated = _mapper.Map<SituationPersonnelleDto>(situationPersonnelle);

            return ServiceResult<SituationPersonnelleDto>.Ok(situationPersonnelleDtoCreated);
        }

        public async Task<ServiceResult> UpdateSituationPersonnelleAsync(int id, UpdateSituationPersonnelleDto situationPersonnelleDto)
        {
            if (id != situationPersonnelleDto.Id)
                return ServiceResult.Fail("L'identifiant de la situation personnelle ne correspond pas à celui de l'objet envoyé", ServiceErrorType.BadRequest);

            var situationPersonnelle = await _situationPersonnelleRepository.GetByIdAsync(id);
            if (situationPersonnelle == null)
                return ServiceResult.Fail("Aucune situation personnelle correspondante n'a été trouvée", ServiceErrorType.NotFound);

            _mapper.Map(situationPersonnelleDto, situationPersonnelle);

            await _situationPersonnelleRepository.UpdateAsync(situationPersonnelle);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteSituationPersonnelleAsync(int id)
        {
            var situationPersonnelle = await _situationPersonnelleRepository.GetByIdAsync(id);
            if (situationPersonnelle == null)
                return ServiceResult.Fail("Aucune situation personnelle correspondante n'a été trouvée", ServiceErrorType.NotFound);

            await _situationPersonnelleRepository.DeleteAsync(id);

            return ServiceResult.Ok();
        }
    }
}