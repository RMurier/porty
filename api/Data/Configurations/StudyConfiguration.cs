using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class StudyConfiguration : IEntityTypeConfiguration<Study>
    {
        public void Configure(EntityTypeBuilder<Study> b)
        {
            b.ToTable("T_STUDY");
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
                .HasMaxLength(512)
                .IsRequired(false);

            b.Property(x => x.RefSchool)
                .HasMaxLength(128)
                .IsRequired();

            b.HasOne(x => x.School)
                .WithMany(x => x.Studies)
                .HasForeignKey(x => x.RefSchool)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}