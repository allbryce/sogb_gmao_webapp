using Sinba.BusinessModel.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Sinba.DataAccess.Mapping
{
    public class FonctionActionMap : EntityTypeConfiguration<FonctionAction>
    {
        public FonctionActionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CodeFonction, t.CodeAction });

            // Properties
            this.Property(t => t.CodeFonction)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CodeAction)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("FonctionAction");
            this.Property(t => t.CodeFonction).HasColumnName("CodeFonction");
            this.Property(t => t.CodeAction).HasColumnName("CodeAction");

            // Relationships
            this.HasRequired(t => t.Fonction)
                .WithMany(t => t.FonctionActions)
                .HasForeignKey(d => d.CodeFonction);
            this.HasRequired(t => t.Action)
                .WithMany(t => t.FonctionActions)
                .HasForeignKey(d => d.CodeAction);

        }
    }
}
