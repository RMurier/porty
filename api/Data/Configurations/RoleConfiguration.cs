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
            b.ToTable("T_ROLE");
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd()
                .HasColumnName("ID")
                .HasColumnOrder(1);

            b.Property(x => x.Name)
                .HasMaxLength(64)
                .HasColumnName("NAME")
                .HasColumnOrder(2)
                .IsRequired();

            b.HasIndex(x => x.Name)
                .IsUnique();

            b.Property(x => x.Description)
                .HasColumnName("DESCRIPTION")
                .HasColumnOrder(3)
                .HasMaxLength(64);

            b.HasData(
                new Role { Id = new Guid("74737d58-a69f-4df7-bf9a-777297a4d6d6"), Name = "User", Description = "Utilisateur standard" },
                new Role { Id = new Guid("a3a52661-a5a1-4d13-a765-48543eb06cfe"), Name = "Admin", Description = "Administrateur" }
            );
        }
    }
}