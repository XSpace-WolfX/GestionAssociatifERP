using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Models;
using GestionAssociatifERP.Repositories;

namespace GestionAssociatifERP.Services
{
    public class ResponsableService : IResponsableService
    {
        private readonly IResponsableRepository _responsableRepository;
        private readonly IMapper _mapper;

        public ResponsableService(IResponsableRepository responsableRepository, IMapper mapper)
        {
            _responsableRepository = responsableRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<ResponsableDto>>> GetAllResponsablesAsync()
        {
            var responsables = await _responsableRepository.GetAllAsync();
            var responsablesDto = _mapper.Map<IEnumerable<ResponsableDto>>(responsables);

            if (responsablesDto == null)
                responsablesDto = new List<ResponsableDto>();

            return ServiceResult<IEnumerable<ResponsableDto>>.Ok(responsablesDto);
        }

        public async Task<ServiceResult<ResponsableDto>> GetResponsableAsync(int id)
        {
            var responsable = await _responsableRepository.GetByIdAsync(id);
            if (responsable == null)
                return ServiceResult<ResponsableDto>.Fail("Aucun responsable correspondant n'a été trouvé", ServiceErrorType.NotFound);

            var responsableDto = _mapper.Map<ResponsableDto>(responsable);

            return ServiceResult<ResponsableDto>.Ok(responsableDto);
        }

        public async Task<ServiceResult<ResponsableWithInformationFinanciereDto>> GetResponsableWithInformationFinanciereAsync(int id)
        {
            var responsable = await _responsableRepository.GetWithInformationFinanciereAsync(id);
            if (responsable == null)
                return ServiceResult<ResponsableWithInformationFinanciereDto>.Fail("Aucun responsable correspondant n'a été trouvé", ServiceErrorType.NotFound);

            var responsableDto = _mapper.Map<ResponsableWithInformationFinanciereDto>(responsable);

            return ServiceResult<ResponsableWithInformationFinanciereDto>.Ok(responsableDto);
        }

        public async Task<ServiceResult<ResponsableWithSituationPersonnelleDto>> GetResponsableWithSituationPersonnelleAsync(int id)
        {
            var responsable = await _responsableRepository.GetWithSituationPersonnelleAsync(id);
            if (responsable == null)
                return ServiceResult<ResponsableWithSituationPersonnelleDto>.Fail("Aucun responsable correspondant n'a été trouvé", ServiceErrorType.NotFound);

            var responsableDto = _mapper.Map<ResponsableWithSituationPersonnelleDto>(responsable);

            return ServiceResult<ResponsableWithSituationPersonnelleDto>.Ok(responsableDto);
        }

        public async Task<ServiceResult<ResponsableWithEnfantsDto>> GetResponsableWithEnfantsAsync(int id)
        {
            var responsable = await _responsableRepository.GetWithEnfantsAsync(id);
            if (responsable == null)
                return ServiceResult<ResponsableWithEnfantsDto>.Fail("Aucun responsable correspondant n'a été trouvé", ServiceErrorType.NotFound);

            var responsableDto = _mapper.Map<ResponsableWithEnfantsDto>(responsable);

            return ServiceResult<ResponsableWithEnfantsDto>.Ok(responsableDto);
        }

        public async Task<ServiceResult<ResponsableDto>> CreateResponsableAsync(CreateResponsableDto responsableDto)
        {
            var responsable = _mapper.Map<Responsable>(responsableDto);
            if (responsable == null)
                return ServiceResult<ResponsableDto>.Fail("Erreur lors de la création du responsable : Le Mapping a échoué", ServiceErrorType.InternalError);

            await _responsableRepository.AddAsync(responsable);

            var responsableCreated = await _responsableRepository.GetByIdAsync(responsable.Id);
            if (responsableCreated == null)
                return ServiceResult<ResponsableDto>.Fail("Échec de la création du responsable", ServiceErrorType.InternalError);

            var responsableDtoCreated = _mapper.Map<ResponsableDto>(responsable);

            return ServiceResult<ResponsableDto>.Ok(responsableDtoCreated);
        }

        public async Task<ServiceResult> UpdateResponsableAsync(int id, UpdateResponsableDto responsableDto)
        {
            if (id != responsableDto.Id)
                return ServiceResult.Fail("L'identifiant du responsable ne correspond pas à celui de l'objet envoyé", ServiceErrorType.BadRequest);

            var responsable = await _responsableRepository.GetByIdAsync(id);
            if (responsable == null)
                return ServiceResult.Fail("Aucun responsable correspondant n'a été trouvé", ServiceErrorType.NotFound);

            _mapper.Map(responsableDto, responsable);

            await _responsableRepository.UpdateAsync(responsable);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteResponsableAsync(int id)
        {
            var responsable = await _responsableRepository.GetByIdAsync(id);
            if (responsable == null)
                return ServiceResult.Fail("Aucun responsable correspondant n'a été trouvé", ServiceErrorType.NotFound);

            await _responsableRepository.DeleteAsync(id);

            return ServiceResult.Ok();
        }
    }
}
