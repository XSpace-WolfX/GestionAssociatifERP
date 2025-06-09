using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAssociatifERP.Models;

[Table("responsable")]
public partial class Responsable
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

    [Column("nom_naissance")]
    [StringLength(100)]
    public string? NomNaissance { get; set; }

    [Column("prenom")]
    [StringLength(100)]
    public string? Prenom { get; set; }

    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("adresse")]
    [StringLength(255)]
    public string? Adresse { get; set; }

    [Column("code_postal")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CodePostal { get; set; }

    [Column("ville")]
    [StringLength(100)]
    public string? Ville { get; set; }

    [Column("telephone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Telephone { get; set; }

    [Column("telephone2")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Telephone2 { get; set; }

    [Column("numero_alloc")]
    [StringLength(50)]
    [Unicode(false)]
    public string? NumeroAlloc { get; set; }

    [InverseProperty("Responsable")]
    public virtual InformationFinanciere? InformationFinanciere { get; set; }

    [InverseProperty("Responsable")]
    public virtual ICollection<ResponsableEnfant> ResponsableEnfants { get; set; } = new List<ResponsableEnfant>();

    [InverseProperty("Responsable")]
    public virtual SituationPersonnelle? SituationPersonnelle { get; set; }
}
