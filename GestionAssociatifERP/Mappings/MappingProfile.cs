using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Models;

namespace GestionAssociatifERP.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Responsable, ResponsableDto>();
            CreateMap<Responsable, ResponsableWithInformationFinanciereDto>();
            CreateMap<Responsable, ResponsableWithSituationPersonnelleDto>();
            CreateMap<Responsable, ResponsableWithEnfantsDto>()
                .ForMember(dest => dest.Enfants, opt => opt.MapFrom(src => src.ResponsableEnfants.Select(re => re.Enfant)));
            CreateMap<CreateResponsableDto, Responsable>();
            CreateMap<UpdateResponsableDto, Responsable>();

            CreateMap<Enfant, EnfantDto>();
            CreateMap<Enfant, EnfantWithResponsablesDto>()
                .ForMember(dest => dest.Responsables, opt => opt.MapFrom(src => src.ResponsableEnfants.Select(re => re.Responsable)));
            CreateMap<Enfant, EnfantWithPersonnesAutoriseesDto>()
                .ForMember(dest => dest.PersonnesAutorisees, opt => opt.MapFrom(src => src.PersonneAutoriseeEnfants.Select(pa => pa.PersonneAutorisee)));
            CreateMap<Enfant, EnfantWithDonneesSupplementairesDto>();
            CreateMap<CreateEnfantDto, Enfant>();
            CreateMap<UpdateEnfantDto, Enfant>();

            CreateMap<InformationFinanciere, InformationFinanciereDto>();
            CreateMap<CreateInformationFinanciereDto, InformationFinanciere>();
            CreateMap<UpdateInformationFinanciereDto, InformationFinanciere>();

            CreateMap<SituationPersonnelle, SituationPersonnelleDto>();
            CreateMap<CreateSituationPersonnelleDto, SituationPersonnelle>();
            CreateMap<UpdateSituationPersonnelleDto, SituationPersonnelle>();

            CreateMap<DonneeSupplementaire, DonneeSupplementaireDto>();
            CreateMap<CreateDonneeSupplementaireDto, DonneeSupplementaire>();
            CreateMap<UpdateDonneeSupplementaireDto, DonneeSupplementaire>();

            CreateMap<PersonneAutorisee, PersonneAutoriseeDto>();
            CreateMap<PersonneAutorisee, PersonneAutoriseeWithEnfantsDto>()
                .ForMember(dest => dest.Enfants, opt => opt.MapFrom(src => src.PersonneAutoriseeEnfants.Select(link => link.Enfant)));
            CreateMap<CreatePersonneAutoriseeDto, PersonneAutorisee>();
            CreateMap<UpdatePersonneAutoriseeDto, PersonneAutorisee>();

            CreateMap<ResponsableEnfant, LinkResponsableEnfantDto>();
            CreateMap<CreateLinkResponsableEnfantDto, ResponsableEnfant>();
            CreateMap<UpdateLinkResponsableEnfantDto, ResponsableEnfant>();

            CreateMap<PersonneAutoriseeEnfant, LinkPersonneAutoriseeEnfantDto>();
            CreateMap<CreateLinkPersonneAutoriseeEnfantDto, PersonneAutoriseeEnfant>();
            CreateMap<UpdateLinkPersonneAutoriseeEnfantDto, PersonneAutoriseeEnfant>();
        }
    }
}
