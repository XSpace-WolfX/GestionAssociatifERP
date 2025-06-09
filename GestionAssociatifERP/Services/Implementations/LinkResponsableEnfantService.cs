using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Helpers;
using GestionAssociatifERP.Models;
using GestionAssociatifERP.Repositories;

namespace GestionAssociatifERP.Services
{
    public class LinkResponsableEnfantService : ILinkResponsableEnfantService
    {
        private readonly IResponsableEnfantRepository _responsableEnfantRepository;
        private readonly IEnfantRepository _enfantRepository;
        private readonly IResponsableRepository _responsableRepository;
        private readonly IMapper _mapper;

        public LinkResponsableEnfantService(IResponsableEnfantRepository responsableEnfantRepository, IEnfantRepository enfantRepository, IResponsableRepository responsableRepository, IMapper mapper)
        {
            _responsableEnfantRepository = responsableEnfantRepository;
            _enfantRepository = enfantRepository;
            _responsableRepository = responsableRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<LinkResponsableEnfantDto>>> GetResponsablesByEnfantIdAsync(int enfantId)
        {
            var exists = await _enfantRepository.ExistsAsync(e => e.Id == enfantId);
            if (!exists)
                return ServiceResult<IEnumerable<LinkResponsableEnfantDto>>.Fail("L'enfant spécifié n'existe pas", ServiceErrorType.NotFound);

            var linkResponsableEnfant = await _responsableEnfantRepository.GetResponsablesByEnfantIdAsync(enfantId);

            var linkResponsableEnfantDto = _mapper.Map<IEnumerable<LinkResponsableEnfantDto>>(linkResponsableEnfant);

            return ServiceResult<IEnumerable<LinkResponsableEnfantDto>>.Ok(linkResponsableEnfantDto);
        }

        public async Task<ServiceResult<IEnumerable<LinkResponsableEnfantDto>>> GetEnfantsByResponsableIdAsync(int responsableId)
        {
            var exists = await _responsableRepository.ExistsAsync(r => r.Id == responsableId);
            if (!exists)
                return ServiceResult<IEnumerable<LinkResponsableEnfantDto>>.Fail("Le responsable spécifié n'existe pas", ServiceErrorType.NotFound);

            var linkResponsableEnfant = await _responsableEnfantRepository.GetEnfantsByResponsableIdAsync(responsableId);

            var linkResponsableEnfantDto = _mapper.Map<IEnumerable<LinkResponsableEnfantDto>>(linkResponsableEnfant);

            return ServiceResult<IEnumerable<LinkResponsableEnfantDto>>.Ok(linkResponsableEnfantDto);
        }

        public async Task<ServiceResult<bool>> ExistsLinkResponsableEnfantAsync(int enfantId, int responsableId)
        {
            var exists = await _responsableEnfantRepository.LinkExistsAsync(responsableId, enfantId);

            return ServiceResult<bool>.Ok(exists);
        }

        public async Task<ServiceResult<LinkResponsableEnfantDto>> CreateLinkResponsableEnfantAsync(CreateLinkResponsableEnfantDto responsableEnfantDto)
        {
            if (!await _enfantRepository.ExistsAsync(e => e.Id == responsableEnfantDto.EnfantId))
                return ServiceResult<LinkResponsableEnfantDto>.Fail("L'enfant spécifié n'existe pas", ServiceErrorType.NotFound);
            else if (!await _responsableRepository.ExistsAsync(r => r.Id == responsableEnfantDto.ResponsableId))
                return ServiceResult<LinkResponsableEnfantDto>.Fail("Le responsable spécifié n'existe pas", ServiceErrorType.NotFound);

            if (await _responsableEnfantRepository.LinkExistsAsync(responsableEnfantDto.ResponsableId, responsableEnfantDto.EnfantId))
                return ServiceResult<LinkResponsableEnfantDto>.Fail("Ce lien existe déjà entre ce responsable et cet enfant", ServiceErrorType.Conflict);

            var responsableEnfant = _mapper.Map<ResponsableEnfant>(responsableEnfantDto);
            if (responsableEnfant == null)
                return ServiceResult<LinkResponsableEnfantDto>.Fail("Erreur lors de la création du lien Responsable / Enfant : Le Mapping a échoué", ServiceErrorType.InternalError);

            await _responsableEnfantRepository.AddAsync(responsableEnfant);

            var createdresponsableEnfant = await _responsableEnfantRepository.GetLinkAsync(responsableEnfantDto.ResponsableId, responsableEnfantDto.EnfantId);
            if (createdresponsableEnfant == null)
                return ServiceResult<LinkResponsableEnfantDto>.Fail("Échec de la création du lien Responsable / Enfant", ServiceErrorType.InternalError);

            var createdResponsableEnfantDto = _mapper.Map<LinkResponsableEnfantDto>(createdresponsableEnfant);

            return ServiceResult<LinkResponsableEnfantDto>.Ok(createdResponsableEnfantDto);
        }

        public async Task<ServiceResult> UpdateLinkResponsableEnfantAsync(UpdateLinkResponsableEnfantDto responsableEnfantDto)
        {
            if (responsableEnfantDto == null) // TODO dans le controller
                return ServiceResult.Fail("Le lien Responsable / Enfant envoyé est vide");

            var responsableEnfant = await _responsableEnfantRepository.GetLinkAsync(responsableEnfantDto.ResponsableId, responsableEnfantDto.EnfantId);
            if (responsableEnfant == null)
                return ServiceResult.Fail("Aucun lien Responsable / Enfant trouvé à mettre à jour", ServiceErrorType.NotFound);

            _mapper.Map(responsableEnfantDto, responsableEnfant);

            await _responsableEnfantRepository.UpdateAsync(responsableEnfant);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> RemoveLinkResponsableEnfantAsync(int enfantId, int responsableId)
        {
            if (!await _responsableEnfantRepository.LinkExistsAsync(responsableId, enfantId))
                return ServiceResult.Fail("Aucun lien Responsable / Enfant trouvé à supprimer", ServiceErrorType.NotFound);

            await _responsableEnfantRepository.RemoveLinkAsync(responsableId, enfantId);

            return ServiceResult.Ok();
        }
    }   
}