using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class MailTemplateConfiguration : IEntityTypeConfiguration<MailTemplate>
    {
        public void Configure(EntityTypeBuilder<MailTemplate> b)
        {
            b.ToTable("T_MAIL_TEMPLATE");

            b.HasKey(x => x.Id);

            b.Property(b => b.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd()
                .HasColumnName("ID")
                .HasColumnOrder(1);

            b.Property(b => b.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME")
                .HasColumnOrder(2)
                .IsRequired();

            b.Property(x => x.Description)
                .HasMaxLength(200)
                .HasColumnName("DESCRIPTION")
                .HasColumnOrder(3)
                .IsUnicode(true);

            b.Property(x => x.IsActive)
                .HasColumnName("IS_ACTIVE")
                .HasColumnOrder(4)
                .HasDefaultValue(true);

            b.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAdd()
                .HasColumnName("CREATED_AT")
                .HasColumnOrder(5);

            b.Property(x => x.ModifiedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("MODIFIED_AT")
                .HasColumnOrder(6);

            b.HasData(
                new MailTemplate() { Id = new Guid("7961e375-87e8-417a-8c67-5717c31d84f1"), Name = "ConfirmationInscription", Description = "Mail envoyé lors de la création du compte avec un token pour valider le compte.", IsActive = true }
            );
        }
    }
}
