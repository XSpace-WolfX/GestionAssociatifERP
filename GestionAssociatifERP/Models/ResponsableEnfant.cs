using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAssociatifERP.Models;

[PrimaryKey("ResponsableId", "EnfantId")]
[Table("responsable_enfant")]
public partial class ResponsableEnfant
{
    [Key]
    [Column("responsable_id")]
    public int ResponsableId { get; set; }

    [Key]
    [Column("enfant_id")]
    public int EnfantId { get; set; }

    [Column("affiliation")]
    [StringLength(50)]
    public string? Affiliation { get; set; }

    [ForeignKey("EnfantId")]
    [InverseProperty("ResponsableEnfants")]
    public virtual Enfant Enfant { get; set; } = null!;

    [ForeignKey("ResponsableId")]
    [InverseProperty("ResponsableEnfants")]
    public virtual Responsable Responsable { get; set; } = null!;
}
