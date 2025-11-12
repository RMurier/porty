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
                .ValueGeneratedOnAdd()
                .HasColumnName("ID")
                .HasColumnOrder(1);

            b.Property(x => x.RefCareer)
                .HasColumnName("REF_CAREER")
                .HasColumnOrder(2)
                .IsRequired();

            b.HasOne(x => x.Career)
                .WithMany(x => x.CareerSkills)
                .HasForeignKey(x => x.RefCareer)
                .OnDelete(DeleteBehavior.Cascade);

            b.Property(x => x.RefSkill)
                .HasColumnName("REF_SKILL")
                .HasColumnOrder(3)
                .IsRequired();

            b.HasOne(x => x.Skill)
                .WithMany(x => x.CareerSkills)
                .HasForeignKey(x => x.RefSkill)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}