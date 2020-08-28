using Sinba.BusinessModel.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Sinba.DataAccess.Mapping
{
    public class ListeActionMap : EntityTypeConfiguration<Action>
    {
        public ListeActionMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Nom)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("ListeAction");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Nom).HasColumnName("Nom");
            this.Property(t => t.Log).HasColumnName("Log");

        }
    }
}
