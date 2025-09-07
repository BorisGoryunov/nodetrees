using Microsoft.EntityFrameworkCore;
using Valetax.Entities;

namespace Valetax.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var node = modelBuilder.Entity<Node>();

        node.HasIndex(x => new { x.TreeId, x.ParentId })
            .IsUnique();
        
        node.Property(x => x.Name)
            .HasMaxLength(150);

        modelBuilder.Entity<Tree>()
            .Property(x => x.Name)
            .HasMaxLength(150);
    }
}
