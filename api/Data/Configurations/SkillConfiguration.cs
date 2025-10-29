using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> b)
        {
            b.ToTable("T_SKILL");
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            b.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            b.Property(x => x.Certification)
                .HasMaxLength(128)
                .IsRequired(false);

            b.Property(x => x.RefCategory)
                .HasMaxLength(128)
                .IsRequired();

            b.HasOne(x => x.Category)
                .WithMany(x => x.Skills)
                .HasForeignKey(x => x.RefCategory)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.CareerSkills)
                .WithOne(x => x.Skill)
                .HasForeignKey(x => x.RefSkill)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.ProjectSkills)
                .WithOne(x => x.Skill)
                .HasForeignKey(x => x.RefSkill)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}