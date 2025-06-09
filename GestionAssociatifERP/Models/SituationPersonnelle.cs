using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAssociatifERP.Models;

[Table("situation_personnelle")]
[Index("ResponsableId", Name = "UQ__situatio__02BC536E52B651A0", IsUnique = true)]
public partial class SituationPersonnelle
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("responsable_id")]
    public int ResponsableId { get; set; }

    [Column("situation_familiale")]
    [StringLength(100)]
    public string? SituationFamiliale { get; set; }

    [Column("secteur")]
    [StringLength(100)]
    public string? Secteur { get; set; }

    [Column("zone")]
    [StringLength(100)]
    public string? Zone { get; set; }

    [Column("regime")]
    [StringLength(100)]
    public string? Regime { get; set; }

    [ForeignKey("ResponsableId")]
    [InverseProperty("SituationPersonnelle")]
    public virtual Responsable Responsable { get; set; } = null!;
}
