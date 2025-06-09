using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Models;
using GestionAssociatifERP.Repositories;

namespace GestionAssociatifERP.Services
{
    public class InformationFinanciereService : IInformationFinanciereService
    {
        private readonly IInformationFinanciereRepository _informationFinanciereRepository;
        private readonly IMapper _mapper;
        public InformationFinanciereService(IInformationFinanciereRepository informationFinanciereRepository, IMapper mapper)
        {
            _informationFinanciereRepository = informationFinanciereRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<InformationFinanciereDto>>> GetAllInformationsFinancieresAsync()
        {
            var informationsFinancieres = await _informationFinanciereRepository.GetAllAsync();
            var informationsFinancieresDto = _mapper.Map<IEnumerable<InformationFinanciereDto>>(informationsFinancieres);

            if (informationsFinancieresDto == null)
                informationsFinancieresDto = new List<InformationFinanciereDto>();

            return ServiceResult<IEnumerable<InformationFinanciereDto>>.Ok(informationsFinancieresDto);
        }

        public async Task<ServiceResult<InformationFinanciereDto>> GetInformationFinanciereAsync(int id)
        {
            var informationFinanciere = await _informationFinanciereRepository.GetByIdAsync(id);
            if (informationFinanciere == null)
                return ServiceResult<InformationFinanciereDto>.Fail("Aucune information financière correspondante n'a été trouvée", ServiceErrorType.NotFound);

            var informationFinanciereDto = _mapper.Map<InformationFinanciereDto>(informationFinanciere);

            return ServiceResult<InformationFinanciereDto>.Ok(informationFinanciereDto);
        }

        public async Task<ServiceResult<InformationFinanciereDto>> CreateInformationFinanciereAsync(CreateInformationFinanciereDto informationFinanciereDto)
        {
            var informationFinanciere = _mapper.Map<InformationFinanciere>(informationFinanciereDto);
            if (informationFinanciere == null)
                return ServiceResult<InformationFinanciereDto>.Fail("Erreur lors de la création de l'information financière : Le Mapping a échoué", ServiceErrorType.InternalError);

            await _informationFinanciereRepository.AddAsync(informationFinanciere);

            var createdInformationFinanciere = await _informationFinanciereRepository.GetByIdAsync(informationFinanciere.Id);
            if (createdInformationFinanciere == null)
                return ServiceResult<InformationFinanciereDto>.Fail("Échec de la création de l'information financière", ServiceErrorType.InternalError);

            var createdInformationFinanciereDto = _mapper.Map<InformationFinanciereDto>(createdInformationFinanciere);

            return ServiceResult<InformationFinanciereDto>.Ok(createdInformationFinanciereDto);
        }

        public async Task<ServiceResult> UpdateInformationFinanciereAsync(int id, UpdateInformationFinanciereDto informationFinanciereDto)
        {
            if (id != informationFinanciereDto.Id)
                return ServiceResult.Fail("L'identifiant de l'information financière ne correspond pas à celui de l'objet envoyé", ServiceErrorType.BadRequest);

            var informationFinanciere = await _informationFinanciereRepository.GetByIdAsync(id);
            if (informationFinanciere == null)
                return ServiceResult.Fail("Aucune information financière correspondante n'a été trouvée", ServiceErrorType.NotFound);

            _mapper.Map(informationFinanciereDto, informationFinanciere);

            await _informationFinanciereRepository.UpdateAsync(informationFinanciere);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteInformationFinanciereAsync(int id)
        {
            var informationFinanciere = await _informationFinanciereRepository.GetByIdAsync(id);
            if (informationFinanciere == null)
                return ServiceResult.Fail("Aucune information financière correspondante n'a été trouvée", ServiceErrorType.NotFound);

            await _informationFinanciereRepository.DeleteAsync(id);

            return ServiceResult.Ok();
        }
    }
}