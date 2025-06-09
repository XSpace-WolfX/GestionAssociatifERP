using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAssociatifERP.Models;

[Table("information_financiere")]
[Index("ResponsableId", Name = "UQ__informat__02BC536E4507DFCE", IsUnique = true)]
public partial class InformationFinanciere
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("responsable_id")]
    public int ResponsableId { get; set; }

    [Column("quotient_familiale")]
    public int? QuotientFamiliale { get; set; }

    [Column("revenu_mensuel", TypeName = "decimal(10, 2)")]
    public decimal? RevenuMensuel { get; set; }

    [Column("revenu_annuel", TypeName = "decimal(10, 2)")]
    public decimal? RevenuAnnuel { get; set; }

    [Column("modele")]
    [StringLength(100)]
    public string? Modele { get; set; }

    [Column("date_debut")]
    public DateOnly? DateDebut { get; set; }

    [Column("date_fin")]
    public DateOnly? DateFin { get; set; }

    [ForeignKey("ResponsableId")]
    [InverseProperty("InformationFinanciere")]
    public virtual Responsable Responsable { get; set; } = null!;
}
