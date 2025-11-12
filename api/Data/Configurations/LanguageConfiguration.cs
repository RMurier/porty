using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> b)
        {
            b.ToTable("T_LANGUAGE");
            b.HasKey(x => x.Id);

            b.Property(b => b.Id)
                .UseIdentityColumn()
                .HasColumnName("ID")
                .HasColumnOrder(1);

            b.Property(x => x.Code)
                .HasMaxLength(10)
                .IsUnicode(true)
                .HasColumnName("CODE")
                .HasColumnOrder(2)
                .IsRequired();

            b.HasIndex(x => x.Code)
                .IsUnique();

            b.Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnName("NAME")
                .HasColumnOrder(3)
                .IsRequired();

            b.HasData(
                new Language { Id = 1, Code = "fr", Name = "Français" },
                new Language { Id = 2, Code = "en", Name = "English" }
            );
        }
    }
}