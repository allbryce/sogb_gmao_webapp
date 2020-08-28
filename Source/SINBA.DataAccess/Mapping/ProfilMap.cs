using Sinba.BusinessModel.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Sinba.DataAccess.Mapping
{
    public class ProfilMap : EntityTypeConfiguration<Profil>
    {
        public ProfilMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nom)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Profil");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nom).HasColumnName("Nom");
        }
    }
}
