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
                .ValueGeneratedOnAdd();

            b.Property(x => x.StartDate)
                .IsRequired();

            b.Property(x => x.EndDate)
                .IsRequired(false);

            b.Property(x => x.Title)
                .HasMaxLength(128)
                .IsRequired();

            b.Property(x => x.Comments)
                .HasMaxLength(512);

            b.Property(x => x.RefBuisness)
                .IsRequired();

            b.HasOne(x => x.Buisness)
                .WithMany(x => x.Careers)
                .HasForeignKey(x => x.RefBuisness)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}