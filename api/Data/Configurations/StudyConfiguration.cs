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
                .HasMaxLength(512)
                .HasColumnName("COMMENTS")
                .HasColumnOrder(5)
                .IsRequired(false);

            b.Property(x => x.RefSchool)
                .HasMaxLength(128)
                .HasColumnName("REF_SCHOOL")
                .HasColumnOrder(6)
                .IsRequired();

            b.HasOne(x => x.School)
                .WithMany(x => x.Studies)
                .HasForeignKey(x => x.RefSchool)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}