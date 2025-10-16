using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> b)
        {
            b.ToTable("T_SCHOOL");
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            b.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            b.HasMany(x => x.Studies)
                .WithOne(x => x.School)
                .HasForeignKey(x => x.RefSchool)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}