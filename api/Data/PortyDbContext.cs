using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api.Data;

public class PortyDbContext : DbContext
{
    // ctor pour l’injection de dépendances (runtime)
    public PortyDbContext(DbContextOptions<PortyDbContext> options) : base(options) { }

    // ctor paramètreless requis pour le design-time sans factory
    public PortyDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        // 1) Essaie variables d'environnement
        var envCs = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
        if (!string.IsNullOrWhiteSpace(envCs))
        {
            optionsBuilder.UseSqlServer(envCs);
            return;
        }

        // 2) Essaie appsettings.json à partir du répertoire courant (dotnet ef)
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")}.json", optional: true)
            .AddEnvironmentVariables();

        // 3) Fallback : si le répertoire courant n'est pas celui du projet (rare),
        // tente aussi le dossier parent (utile si tu lances la commande depuis la solution)
        var config = builder.Build();
        var cs = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(cs))
        {
            // second essai : parent
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

        // 4) Dernier recours : hard-fallback (évite NullRef mais échouera proprement si la DB est introuvable)
        cs ??= "Server=.;Database=Porty;User Id=PORTY;Password=IUSR_PORTY;TrustServerCertificate=True;MultipleActiveResultSets=True";

        optionsBuilder.UseSqlServer(cs);

#if DEBUG
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
#endif
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
}
