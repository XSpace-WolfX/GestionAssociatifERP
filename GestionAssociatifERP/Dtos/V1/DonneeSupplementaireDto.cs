namespace GestionAssociatifERP.Dtos.V1
{
    public class DonneeSupplementaireDto
    {
        public int Id { get; set; }
        public int EnfantId { get; set; }
        public string? Parametre { get; set; }
        public string? Valeur { get; set; }
        public string? Type { get; set; }
        public string? Commentaire { get; set; }
    }

    public class CreateDonneeSupplementaireDto
    {
        public int EnfantId { get; set; }
        public string? Parametre { get; set; }
        public string? Valeur { get; set; }
        public string? Type { get; set; }
        public string? Commentaire { get; set; }
    }

    public class UpdateDonneeSupplementaireDto
    {
        public int Id { get; set; }
        public int EnfantId { get; set; }
        public string? Parametre { get; set; }
        public string? Valeur { get; set; }
        public string? Type { get; set; }
        public string? Commentaire { get; set; }
    }
}
