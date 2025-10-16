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
                .ValueGeneratedOnAdd();

            b.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            b.Property(x => x.Description)
                .HasMaxLength(512)
                .IsRequired(false);

            b.Property(x => x.Url)
                .HasMaxLength(256)
                .IsRequired(false);
        }
    }
}