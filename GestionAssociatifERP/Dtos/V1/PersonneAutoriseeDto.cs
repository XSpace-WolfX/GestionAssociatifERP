namespace GestionAssociatifERP.Dtos.V1
{
    public class PersonneAutoriseeDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Telephone { get; set; }
    }

    public class PersonneAutoriseeWithEnfantsDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Telephone { get; set; }

        public List<EnfantDto> Enfants { get; set; } = new();
    }

    public class CreatePersonneAutoriseeDto
    {
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Telephone { get; set; }
    }

    public class UpdatePersonneAutoriseeDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Telephone { get; set; }
    }
}
