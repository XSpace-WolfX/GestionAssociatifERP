using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAssociatifERP.Models;

[PrimaryKey("PersonneAutoriseeId", "EnfantId")]
[Table("personne_autorisee_enfant")]
public partial class PersonneAutoriseeEnfant
{
    [Key]
    [Column("personne_autorisee_id")]
    public int PersonneAutoriseeId { get; set; }

    [Key]
    [Column("enfant_id")]
    public int EnfantId { get; set; }

    [Column("affiliation")]
    [StringLength(100)]
    public string? Affiliation { get; set; }

    [Column("contact_urgence")]
    public bool? ContactUrgence { get; set; }

    [Column("commentaire")]
    [StringLength(100)]
    public string? Commentaire { get; set; }

    [ForeignKey("EnfantId")]
    [InverseProperty("PersonneAutoriseeEnfants")]
    public virtual Enfant Enfant { get; set; } = null!;

    [ForeignKey("PersonneAutoriseeId")]
    [InverseProperty("PersonneAutoriseeEnfants")]
    public virtual PersonneAutorisee PersonneAutorisee { get; set; } = null!;
}
