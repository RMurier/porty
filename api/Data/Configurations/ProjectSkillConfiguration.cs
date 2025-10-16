using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class ProjectSkillConfiguration : IEntityTypeConfiguration<ProjectSkill>
    {
        public void Configure(EntityTypeBuilder<ProjectSkill> b)
        {
            b.ToTable("T_PROJECT_SKILL");
            b.HasKey(x => x.Id);

            b.Property(x => x.RefProject)
                .IsRequired();

            b.HasOne(x => x.Project)
                .WithMany(x => x.ProjectSkills)
                .HasForeignKey(x => x.RefProject)
                .OnDelete(DeleteBehavior.Cascade);

            b.Property(x => x.RefSkill)
                .IsRequired();

            b.HasOne(x => x.Skill)
                .WithMany(x => x.ProjectSkills)
                .HasForeignKey(x => x.RefSkill)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}