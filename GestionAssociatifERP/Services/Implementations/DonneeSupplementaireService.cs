using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Models;
using GestionAssociatifERP.Repositories;

namespace GestionAssociatifERP.Services
{
    public class DonneeSupplementaireService : IDonneeSupplementaireService
    {
        private readonly IDonneeSupplementaireRepository _donneeSupplementaireRepository;
        private readonly IMapper _mapper;

        public DonneeSupplementaireService(IDonneeSupplementaireRepository donneeSupplementaireRepository, IMapper mapper)
        {
            _donneeSupplementaireRepository = donneeSupplementaireRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<DonneeSupplementaireDto>>> GetAllDonneesSupplementairesAsync()
        {
            var donneesSupplementaires = await _donneeSupplementaireRepository.GetAllAsync();
            var donneesSupplementairesDto = _mapper.Map<IEnumerable<DonneeSupplementaireDto>>(donneesSupplementaires);

            if (donneesSupplementairesDto == null)
                donneesSupplementairesDto = new List<DonneeSupplementaireDto>();

            return ServiceResult<IEnumerable<DonneeSupplementaireDto>>.Ok(donneesSupplementairesDto);
        }

        public async Task<ServiceResult<DonneeSupplementaireDto>> GetDonneeSupplementaireAsync(int id)
        {
            var donneeSupplementaire = await _donneeSupplementaireRepository.GetByIdAsync(id);
            if (donneeSupplementaire == null)
                return ServiceResult<DonneeSupplementaireDto>.Fail("Aucune donnée supplémentaire correspondante n'a été trouvée", ServiceErrorType.NotFound);

            var donneeSupplementaireDto = _mapper.Map<DonneeSupplementaireDto>(donneeSupplementaire);

            return ServiceResult<DonneeSupplementaireDto>.Ok(donneeSupplementaireDto);
        }

        public async Task<ServiceResult<DonneeSupplementaireDto>> CreateDonneeSupplementaireAsync(CreateDonneeSupplementaireDto donneeSupplementaireDto)
        {
            var donneeSupplementaire = _mapper.Map<DonneeSupplementaire>(donneeSupplementaireDto);
            if (donneeSupplementaire == null)
                return ServiceResult<DonneeSupplementaireDto>.Fail("Erreur lors de la création de la donnée supplémentaire : Le Mapping a échoué", ServiceErrorType.InternalError);

            await _donneeSupplementaireRepository.AddAsync(donneeSupplementaire);

            var createdDonneeSupplementaire = await _donneeSupplementaireRepository.GetByIdAsync(donneeSupplementaire.Id);
            if (createdDonneeSupplementaire == null)
                return ServiceResult<DonneeSupplementaireDto>.Fail("Échec de la création de la donnée supplémentaire", ServiceErrorType.InternalError);

            var CreatedDonneeSupplementaireDto = _mapper.Map<DonneeSupplementaireDto>(createdDonneeSupplementaire);

            return ServiceResult<DonneeSupplementaireDto>.Ok(CreatedDonneeSupplementaireDto);
        }

        public async Task<ServiceResult> UpdateDonneeSupplementaireAsync(int id, UpdateDonneeSupplementaireDto donneeSupplementaireDto)
        {
            if (id != donneeSupplementaireDto.Id)
                return ServiceResult.Fail("L'identifiant de la donnée supplémentaire ne correspond pas à celui de l'objet envoyé", ServiceErrorType.BadRequest);

            var donneeSupplementaire = await _donneeSupplementaireRepository.GetByIdAsync(id);
            if (donneeSupplementaire == null)
                return ServiceResult.Fail("Aucune donnée supplémentaire correspondante n'a été trouvée", ServiceErrorType.NotFound);

            _mapper.Map(donneeSupplementaireDto, donneeSupplementaire);

            await _donneeSupplementaireRepository.UpdateAsync(donneeSupplementaire);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteDonneeSupplementaireAsync(int id)
        {
            var donneeSupplementaire = await _donneeSupplementaireRepository.GetByIdAsync(id);
            if (donneeSupplementaire == null)
                return ServiceResult.Fail("Aucune donnée supplémentaire correspondante n'a été trouvée", ServiceErrorType.NotFound);

            await _donneeSupplementaireRepository.DeleteAsync(id);

            return ServiceResult.Ok();
        }
    }
}