using api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable("T_USER");

            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnOrder(1)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            b.Property(x => x.FirstName)
                .HasMaxLength(100)
                .HasColumnName("firstName")
                .HasColumnOrder(2)
                .IsRequired();

            b.Property(x => x.LastName)
                .HasMaxLength(100)
                .HasColumnName("lastName")
                .HasColumnOrder(3)
                .IsRequired();

            b.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phoneNumber")
                .HasColumnOrder(4)
                .IsRequired();

            b.Property(x => x.Email)
                .HasMaxLength(256)
                .HasColumnName("email")
                .HasColumnOrder(5)
                .IsRequired();

            b.Property(x => x.Password)
                .HasMaxLength(512)
                .HasColumnName("password")
                .HasColumnOrder(6)
                .IsRequired();

            b.Property(x => x.Pepper)
                .HasMaxLength(64)
                .HasColumnName("pepper")
                .HasColumnOrder(7)
                .IsRequired();

            b.Property(x => x.RefRole)
                .HasColumnName("refRole")
                .HasColumnOrder(8)
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("createdAt")
                .HasColumnOrder(9)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            b.Property(x => x.TokenAccountCreated)
                .HasColumnName("tokenAccountCreated")
                .HasColumnOrder(10);

            b.HasIndex(x => x.Email)
                .IsUnique();

            b.HasOne(x => x.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(x => x.RefRole)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_T_USER_refRole__T_ROLE_id");
        }
    }
}