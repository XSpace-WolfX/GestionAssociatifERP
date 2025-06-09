using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAssociatifERP.Models;

[Table("personne_autorisee")]
public partial class PersonneAutorisee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nom")]
    [StringLength(100)]
    public string? Nom { get; set; }

    [Column("prenom")]
    [StringLength(100)]
    public string? Prenom { get; set; }

    [Column("telephone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Telephone { get; set; }

    [InverseProperty("PersonneAutorisee")]
    public virtual ICollection<PersonneAutoriseeEnfant> PersonneAutoriseeEnfants { get; set; } = new List<PersonneAutoriseeEnfant>();
}
