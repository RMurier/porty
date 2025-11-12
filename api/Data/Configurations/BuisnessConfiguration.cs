using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class BuisnessConfiguration : IEntityTypeConfiguration<Buisness>
    {
        public void Configure(EntityTypeBuilder<Buisness> b)
        {

            b.ToTable("T_BUISNESS");
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd()
                .HasColumnOrder(1);

            b.Property(x => x.Name)
                .HasMaxLength(128)
                .HasColumnName("NAME")
                .HasColumnOrder(2)
                .IsRequired();

            b.HasMany(x => x.Careers)
                .WithOne(x => x.Buisness)
                .HasForeignKey(x => x.RefBuisness)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
