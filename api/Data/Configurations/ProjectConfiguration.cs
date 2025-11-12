using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> b)
        {
            b.ToTable("T_PROJECT");
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd()
                .HasColumnName("ID")
                .HasColumnOrder(1);

            b.Property(x => x.Name)
                .HasMaxLength(128)
                .HasColumnName("NAME")
                .HasColumnOrder(2)
                .IsRequired();

            b.Property(x => x.Description)
                .HasMaxLength(512)
                .HasColumnName("DESCRIPTION")
                .HasColumnOrder(3)
                .IsRequired(false);

            b.Property(x => x.Url)
                .HasMaxLength(256)
                .HasColumnName("URL")
                .HasColumnOrder(5)
                .IsRequired(false);
        }
    }
}