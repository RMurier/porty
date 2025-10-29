using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class CareerSkillConfiguration : IEntityTypeConfiguration<CareerSkill>
    {
        public void Configure(EntityTypeBuilder<CareerSkill> b)
        {
            b.ToTable("T_CAREER_SKILL");
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            b.Property(x => x.RefCareer)
                .IsRequired();

            b.HasOne(x => x.Career)
                .WithMany(x => x.CareerSkills)
                .HasForeignKey(x => x.RefCareer)
                .OnDelete(DeleteBehavior.Cascade);

            b.Property(x => x.RefSkill)
                .IsRequired();

            b.HasOne(x => x.Skill)
                .WithMany(x => x.CareerSkills)
                .HasForeignKey(x => x.RefSkill)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}