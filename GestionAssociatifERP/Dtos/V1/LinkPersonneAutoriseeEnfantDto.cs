namespace GestionAssociatifERP.Dtos.V1
{
    public class LinkPersonneAutoriseeEnfantDto
    {
        public int PersonneAutoriseeId { get; set; }
        public int EnfantId { get; set; }
        public string? Affiliation { get; set; }
        public bool? ContactUrgence { get; set; }
        public string? Commentaire { get; set; }
    }

    public class CreateLinkPersonneAutoriseeEnfantDto
    {
        public int PersonneAutoriseeId { get; set; }
        public int EnfantId { get; set; }
        public string? Affiliation { get; set; }
        public bool? ContactUrgence { get; set; }
        public string? Commentaire { get; set; }
    }

    public class UpdateLinkPersonneAutoriseeEnfantDto
    {
        public int PersonneAutoriseeId { get; set; }
        public int EnfantId { get; set; }
        public string? Affiliation { get; set; }
        public bool? ContactUrgence { get; set; }
        public string? Commentaire { get; set; }
    }
}