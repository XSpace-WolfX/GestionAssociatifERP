using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAssociatifERP.Models;

[Table("enfant")]
public partial class Enfant
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("civilite")]
    [StringLength(255)]
    public string Civilite { get; set; } = null!;

    [Column("nom")]
    [StringLength(100)]
    public string? Nom { get; set; }

    [Column("prenom")]
    [StringLength(100)]
    public string? Prenom { get; set; }

    [Column("date_naissance")]
    public DateOnly? DateNaissance { get; set; }

    [Column("presence_fratrie")]
    public bool? PresenceFratrie { get; set; }

    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("telephone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Telephone { get; set; }

    [Column("ville_naissance")]
    [StringLength(100)]
    public string? VilleNaissance { get; set; }

    [InverseProperty("Enfant")]
    public virtual ICollection<DonneeSupplementaire> DonneeSupplementaires { get; set; } = new List<DonneeSupplementaire>();

    [InverseProperty("Enfant")]
    public virtual ICollection<PersonneAutoriseeEnfant> PersonneAutoriseeEnfants { get; set; } = new List<PersonneAutoriseeEnfant>();

    [InverseProperty("Enfant")]
    public virtual ICollection<ResponsableEnfant> ResponsableEnfants { get; set; } = new List<ResponsableEnfant>();
}
