using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable("T_USER");

            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd()
                .HasColumnName("ID")
                .HasColumnOrder(1);

            b.Property(x => x.FirstName)
                .HasMaxLength(100)
                .HasColumnName("FIRSTNAME")
                .HasColumnOrder(2)
                .IsRequired();

            b.Property(x => x.LastName)
                .HasMaxLength(100)
                .HasColumnName("LASTNAME")
                .HasColumnOrder(3)
                .IsRequired();

            b.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("PHONE_NUMBER")
                .HasColumnOrder(4)
                .IsRequired(false);

            b.Property(x => x.Email)
                .HasMaxLength(256)
                .HasColumnName("EMAIL")
                .HasColumnOrder(5)
                .IsRequired();

            b.HasIndex(x => x.Email).IsUnique();

            b.Property(x => x.Password)
                .HasMaxLength(512)
                .HasColumnName("PASSWORD")
                .HasColumnOrder(6)
                .IsRequired();

            b.Property(x => x.Salt)
                .HasMaxLength(64)
                .HasColumnName("SALT")
                .HasColumnOrder(7)
                .IsRequired();

            b.Property(x => x.RefRole)
                .HasColumnName("REF_ROLE")
                .HasColumnOrder(8)
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("CREATED_AT")
                .HasColumnOrder(9)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            b.Property(x => x.IsEmailValidated)
                .HasColumnName("IS_EMAIL_VALIDATED")
                .HasColumnOrder(10)
                .HasDefaultValue(false);

            b.Property(x => x.TokenAccountCreated)
                .HasColumnName("TOKEN_ACCOUNT_CREATED")
                .HasColumnOrder(11)
                .IsRequired(false);

            b.HasOne(x => x.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(x => x.RefRole)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_T_USER_refRole__T_ROLE_id");
        }
    }
}