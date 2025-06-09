namespace GestionAssociatifERP.Dtos.V1
{
    public class SituationPersonnelleDto
    {
        public int Id { get; set; }
        public int ResponsableId { get; set; }
        public string? SituationFamiliale { get; set; }
        public string? Secteur { get; set; }
        public string? Zone { get; set; }
        public string? Regime { get; set; }
    }

    public class CreateSituationPersonnelleDto
    {
        public int ResponsableId { get; set; }
        public string? SituationFamiliale { get; set; }
        public string? Secteur { get; set; }
        public string? Zone { get; set; }
        public string? Regime { get; set; }
    }

    public class UpdateSituationPersonnelleDto
    {
        public int Id { get; set; }
        public int ResponsableId { get; set; }
        public string? SituationFamiliale { get; set; }
        public string? Secteur { get; set; }
        public string? Zone { get; set; }
        public string? Regime { get; set; }
    }
}
