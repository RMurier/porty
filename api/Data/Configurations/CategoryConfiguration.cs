using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("T_CATEGORY");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd()
                .HasColumnName("ID")
                .HasColumnOrder(1);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME")
                .HasColumnOrder(2)
                .IsRequired();

            builder.HasMany(x => x.Skills)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.RefCategory)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}