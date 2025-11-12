using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class CareerConfiguration : IEntityTypeConfiguration<Career>
    {
        public void Configure(EntityTypeBuilder<Career> b)
        {
            b.ToTable("T_CAREER");
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd()
                .HasColumnName("ID")
                .HasColumnOrder(1);

            b.Property(x => x.StartDate)
                .HasColumnName("START_DATE")
                .HasColumnOrder(2)
                .IsRequired();

            b.Property(x => x.EndDate)
                .HasColumnName("END_DATE")
                .HasColumnOrder(3)
                .IsRequired(false);

            b.Property(x => x.Title)
                .HasMaxLength(128)
                .HasColumnName("TITLE")
                .HasColumnOrder(4)
                .IsRequired();

            b.Property(x => x.Comments)
                .HasColumnName("COMMENTS")
                .HasColumnOrder(5)
                .HasMaxLength(512);

            b.Property(x => x.RefBuisness)
                .HasColumnName("REF_BUISNESS")
                .HasColumnOrder(6)
                .IsRequired();

            b.HasOne(x => x.Buisness)
                .WithMany(x => x.Careers)
                .HasForeignKey(x => x.RefBuisness)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}