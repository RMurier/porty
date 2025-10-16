using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace api.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> b)
        {
            // Convertit la propriété string <-> colonne uniqueidentifier
            var guidStringConverter = new ValueConverter<string, Guid>(
                toProvider => Guid.Parse(toProvider),
                fromProvider => fromProvider.ToString()
            );

            b.ToTable("T_ROLE");
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            b.Property(x => x.Name)
                .HasMaxLength(64)
                .IsRequired();

            b.HasIndex(x => x.Name).
                IsUnique();

            b.Property(x => x.Description)
                .HasMaxLength(64);

            // (Optionnel) Seed avec GUIDs stables
            b.HasData(
                new Role { Id = "74737d58-a69f-4df7-bf9a-777297a4d6d6", Name = "User", Description = "Utilisateur standard" },
                new Role { Id = "74737d58-a69f-4df7-bf9a-777297a4d6d6", Name = "Admin", Description = "Administrateur" }
            );
        }
    }
}