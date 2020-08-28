using Sinba.BusinessModel.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Sinba.DataAccess.Mapping
{
    public class UserRightMap : EntityTypeConfiguration<UserRight>
    {
        public UserRightMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdUser, t.CodeFonction, t.CodeAction });

            // Properties
            this.Property(t => t.IdUser)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.CodeFonction)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CodeAction)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("UserRight");
            this.Property(t => t.IdUser).HasColumnName("IdUser");
            this.Property(t => t.CodeFonction).HasColumnName("CodeFonction");
            this.Property(t => t.CodeAction).HasColumnName("CodeAction");
            this.Property(t => t.DenyAccess).HasColumnName("DenyAccess");

            // Relationships
            this.HasRequired(t => t.FonctionAction)
                .WithMany(t => t.UserRights)
                .HasForeignKey(d => new { d.CodeFonction, d.CodeAction });
            this.HasRequired(t => t.SinbaUser)
                .WithMany(t => t.UserRights)
                .HasForeignKey(d => d.IdUser);

        }
    }
}
