namespace GestionAssociatifERP.Dtos.V1
{
    public class InformationFinanciereDto
    {
        public int Id { get; set; }
        public int ResponsableId { get; set; }
        public int? QuotientFamiliale { get; set; }
        public decimal? RevenuMensuel { get; set; }
        public decimal? RevenuAnnuel { get; set; }
        public string? Modele { get; set; }
        public DateOnly? DateDebut { get; set; }
        public DateOnly? DateFin { get; set; }
    }

    public class CreateInformationFinanciereDto
    {
        public int ResponsableId { get; set; }
        public int? QuotientFamiliale { get; set; }
        public decimal? RevenuMensuel { get; set; }
        public decimal? RevenuAnnuel { get; set; }
        public string? Modele { get; set; }
        public DateOnly? DateDebut { get; set; }
        public DateOnly? DateFin { get; set; }
    }

    public class UpdateInformationFinanciereDto
    {
        public int Id { get; set; }
        public int ResponsableId { get; set; }
        public int? QuotientFamiliale { get; set; }
        public decimal? RevenuMensuel { get; set; }
        public decimal? RevenuAnnuel { get; set; }
        public string? Modele { get; set; }
        public DateOnly? DateDebut { get; set; }
        public DateOnly? DateFin { get; set; }
    }
}
