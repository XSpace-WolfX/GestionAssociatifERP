namespace GestionAssociatifERP.Dtos.V1
{
    public class ResponsableDto
    {
        public int Id { get; set; }
        public string Civilite { get; set; } = string.Empty;
        public string? Nom { get; set; }
        public string? NomNaissance { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Telephone { get; set; }
        public string? Telephone2 { get; set; }
        public string? NumeroAlloc { get; set; }
    }

    public class ResponsableWithInformationFinanciereDto
    {
        public int Id { get; set; }
        public string Civilite { get; set; } = string.Empty;
        public string? Nom { get; set; }
        public string? NomNaissance { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Telephone { get; set; }
        public string? Telephone2 { get; set; }
        public string? NumeroAlloc { get; set; }

        public InformationFinanciereDto? InformationFinanciere { get; set; }
    }

    public class ResponsableWithSituationPersonnelleDto
    {
        public int Id { get; set; }
        public string Civilite { get; set; } = string.Empty;
        public string? Nom { get; set; }
        public string? NomNaissance { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Telephone { get; set; }
        public string? Telephone2 { get; set; }
        public string? NumeroAlloc { get; set; }

        public SituationPersonnelleDto? SituationPersonnelle { get; set; }
    }

    public class ResponsableWithEnfantsDto
    {
        public int Id { get; set; }
        public string Civilite { get; set; } = string.Empty;
        public string? Nom { get; set; }
        public string? NomNaissance { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Telephone { get; set; }
        public string? Telephone2 { get; set; }
        public string? NumeroAlloc { get; set; }

        public List<EnfantDto> Enfants { get; set; } = new();
    }

    public class CreateResponsableDto
    {
        public string Civilite { get; set; } = string.Empty;
        public string? Nom { get; set; }
        public string? NomNaissance { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Telephone { get; set; }
        public string? Telephone2 { get; set; }
        public string? NumeroAlloc { get; set; }
    }

    public class UpdateResponsableDto
    {
        public int Id { get; set; }
        public string Civilite { get; set; } = string.Empty;
        public string? Nom { get; set; }
        public string? NomNaissance { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Telephone { get; set; }
        public string? Telephone2 { get; set; }
        public string? NumeroAlloc { get; set; }
    }
}
