using Microsoft.EntityFrameworkCore;

namespace GestionAssociatifERP.Models;

public partial class GestionAssociatifDbContext : DbContext
{
    public GestionAssociatifDbContext()
    {
    }

    public GestionAssociatifDbContext(DbContextOptions<GestionAssociatifDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DonneeSupplementaire> DonneeSupplementaires { get; set; }

    public virtual DbSet<Enfant> Enfants { get; set; }

    public virtual DbSet<InformationFinanciere> InformationFinancieres { get; set; }

    public virtual DbSet<PersonneAutorisee> PersonneAutorisees { get; set; }

    public virtual DbSet<PersonneAutoriseeEnfant> PersonneAutoriseeEnfants { get; set; }

    public virtual DbSet<Responsable> Responsables { get; set; }

    public virtual DbSet<ResponsableEnfant> ResponsableEnfants { get; set; }

    public virtual DbSet<SituationPersonnelle> SituationPersonnelles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) { /* Ne rien faire ici. La configuration vient de l’extérieur (injection dans le startup/program) */ }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DonneeSupplementaire>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__donnee_s__3213E83FCB0923C0");

            entity.HasOne(d => d.Enfant).WithMany(p => p.DonneeSupplementaires)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__donnee_su__enfan__6E01572D");
        });

        modelBuilder.Entity<Enfant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__enfant__3213E83F21B6F534");
        });

        modelBuilder.Entity<InformationFinanciere>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__informat__3213E83F2EC9DD4D");

            entity.HasOne(d => d.Responsable).WithOne(p => p.InformationFinanciere)
                .HasForeignKey<InformationFinanciere>(d => d.ResponsableId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__informati__respo__6EF57B66");
        });

        modelBuilder.Entity<PersonneAutorisee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personne__3213E83FBB21BAC4");
        });

        modelBuilder.Entity<PersonneAutoriseeEnfant>(entity =>
        {
            entity.HasKey(e => new { e.PersonneAutoriseeId, e.EnfantId }).HasName("PK__personne__D627FA0E735E9FB4");

            entity.HasOne(d => d.Enfant).WithMany(p => p.PersonneAutoriseeEnfants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__personne___enfan__72C60C4A");

            entity.HasOne(d => d.PersonneAutorisee).WithMany(p => p.PersonneAutoriseeEnfants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__personne___perso__71D1E811");
        });

        modelBuilder.Entity<Responsable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__responsa__3213E83F3AA359CD");
        });

        modelBuilder.Entity<ResponsableEnfant>(entity =>
        {
            entity.HasKey(e => new { e.ResponsableId, e.EnfantId }).HasName("PK__responsa__CB2A4AEE6C215CCB");

            entity.HasOne(d => d.Enfant).WithMany(p => p.ResponsableEnfants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__responsab__enfan__70DDC3D8");

            entity.HasOne(d => d.Responsable).WithMany(p => p.ResponsableEnfants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__responsab__respo__6FE99F9F");
        });

        modelBuilder.Entity<SituationPersonnelle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__situatio__3213E83F1F894EA8");

            entity.HasOne(d => d.Responsable).WithOne(p => p.SituationPersonnelle)
                .HasForeignKey<SituationPersonnelle>(d => d.ResponsableId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__situation__respo__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
