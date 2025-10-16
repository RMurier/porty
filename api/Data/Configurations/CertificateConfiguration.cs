using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> b)
        {
            b.ToTable("T_CERTIFICATE");
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            b.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            b.Property(x => x.Date)
                .HasColumnType("datetime2")
                .IsRequired(false);

            b.Property(x => x.Url)
                .HasMaxLength(256)
                .IsRequired(false);
        }
    }
}