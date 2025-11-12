using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class MailTemplateTranslationConfiguration : IEntityTypeConfiguration<MailTemplateTranslation>
    {
        public void Configure(EntityTypeBuilder<MailTemplateTranslation> b)
        {
            b.ToTable("T_MAIL_TEMPLATE_TRANSLATION");

            b.HasKey(x => x.Id);

            b.Property(b => b.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd()
                .HasColumnName("ID")
                .HasColumnOrder(1);

            b.Property(x => x.Locale)
                .HasMaxLength(10)
                .IsUnicode(true)
                .HasColumnName("LOCALE")
                .HasColumnOrder(2)
                .IsRequired();

            b.Property(x => x.Subject)
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasColumnName("SUBJECT")
                .HasColumnOrder(3)
                .IsRequired();

            b.Property(x => x.HtmlBody)
                .IsUnicode(true)
                .HasColumnName("HTML_BODY")
                .HasColumnOrder(4)
                .IsRequired();

            b.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("UPDATED_AT")
                .HasColumnOrder(6);

            b.Property(x => x.RefTemplate)
                .HasColumnName("REF_TEMPLATE")
                .HasColumnOrder(7)
                .IsRequired();

            b.HasIndex(x => new { x.RefTemplate, x.Locale }).IsUnique();

            b.HasOne(x => x.Template)
                .WithMany(t => t.Translations)
                .HasForeignKey(x => x.RefTemplate)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne<Language>()
                .WithMany()
                .HasForeignKey(x => x.Locale)
                .HasPrincipalKey(l => l.Code)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
