namespace GestionAssociatifERP.Dtos.V1
{
    public class LinkResponsableEnfantDto
    {
        public int ResponsableId { get; set; }
        public int EnfantId { get; set; }
        public string? Affiliation { get; set; }
    }

    public class CreateLinkResponsableEnfantDto
    {
        public int ResponsableId { get; set; }
        public int EnfantId { get; set; }
        public string? Affiliation { get; set; }
    }

    public class UpdateLinkResponsableEnfantDto
    {
        public int ResponsableId { get; set; }
        public int EnfantId { get; set; }
        public string? Affiliation { get; set; }
    }
}
