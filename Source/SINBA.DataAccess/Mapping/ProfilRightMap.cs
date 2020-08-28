using Sinba.BusinessModel.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sinba.DataAccess.Mapping
{
    public class ProfilRightMap : EntityTypeConfiguration<ProfilRight>
    {
        public ProfilRightMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdProfil, t.CodeFonction, t.CodeAction });

            // Properties
            this.Property(t => t.IdProfil)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodeFonction)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CodeAction)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ProfilRight");
            this.Property(t => t.IdProfil).HasColumnName("IdProfil");
            this.Property(t => t.CodeFonction).HasColumnName("CodeFonction");
            this.Property(t => t.CodeAction).HasColumnName("CodeAction");

            // Relationships
            this.HasRequired(t => t.FonctionAction)
                .WithMany(t => t.ProfilRights)
                .HasForeignKey(d => new { d.CodeFonction, d.CodeAction });
            this.HasRequired(t => t.Profil)
                .WithMany(t => t.ProfilRights)
                .HasForeignKey(d => d.IdProfil);
        }
    }
}
