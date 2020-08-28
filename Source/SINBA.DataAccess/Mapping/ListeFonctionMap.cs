using Sinba.BusinessModel.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Sinba.DataAccess.Mapping
{
    public class ListeFonctionMap : EntityTypeConfiguration<Fonction>
    {
        public ListeFonctionMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ListeFonction");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.MenuPath).HasColumnName("MenuPath");
            this.Property(t => t.SuperAdmin).HasColumnName("SA");
            this.Property(t => t.Log).HasColumnName("Log");
        }
    }
}
