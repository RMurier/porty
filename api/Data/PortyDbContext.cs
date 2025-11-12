using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api.Data;

public class PortyDbContext : DbContext
{
    public PortyDbContext(DbContextOptions<PortyDbContext> options) : base(options) { }

    public PortyDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        var envCs = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
        if (!string.IsNullOrWhiteSpace(envCs))
        {
            optionsBuilder.UseSqlServer(envCs);
            return;
        }

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")}.json", optional: true)
            .AddEnvironmentVariables();

        var config = builder.Build();
        var cs = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(cs))
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
            if (parent is not null)
            {
                var cfg2 = new ConfigurationBuilder()
                    .SetBasePath(parent)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();

                cs = cfg2.GetConnectionString("DefaultConnection");
            }
        }


        optionsBuilder.UseSqlServer(cs);

        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortyDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    // DbSets
    public DbSet<Buisness> Buisnesses => Set<Buisness>();
    public DbSet<Career> Careers => Set<Career>();
    public DbSet<CareerSkill> CareerSkills => Set<CareerSkill>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Certificate> Certificates => Set<Certificate>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectSkill> ProjectSkills => Set<ProjectSkill>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<School> Schools => Set<School>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Study> Studies => Set<Study>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<MailTemplate> MailTemplates => Set<MailTemplate>();
    public DbSet<MailTemplateTranslation> MailTemplatesTranslation => Set<MailTemplateTranslation>();
}
