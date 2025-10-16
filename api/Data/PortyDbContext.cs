using api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class PortyDbContext : DbContext
{
    public PortyDbContext(DbContextOptions<PortyDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortyDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    // DbSet si besoin
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
}
