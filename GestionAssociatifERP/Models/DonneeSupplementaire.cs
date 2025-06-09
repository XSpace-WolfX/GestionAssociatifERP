using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAssociatifERP.Models;

[Table("donnee_supplementaire")]
public partial class DonneeSupplementaire
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("enfant_id")]
    public int EnfantId { get; set; }

    [Column("parametre")]
    [StringLength(100)]
    public string? Parametre { get; set; }

    [Column("valeur")]
    public string? Valeur { get; set; }

    [Column("type")]
    [StringLength(50)]
    public string? Type { get; set; }

    [Column("commentaire")]
    [StringLength(100)]
    public string? Commentaire { get; set; }

    [ForeignKey("EnfantId")]
    [InverseProperty("DonneeSupplementaires")]
    public virtual Enfant Enfant { get; set; } = null!;
}
