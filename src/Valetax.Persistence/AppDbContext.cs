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
        node.ToTable(nameof(Node));

        node.HasIndex(x => new { x.TreeId, x.Id })
            .IsUnique();

        node.Property(x => x.Name)
            .HasMaxLength(150);

        modelBuilder.Entity<Tree>()
            .ToTable(nameof(Tree))
            .Property(x => x.Name)
            .HasMaxLength(150);

        modelBuilder.Entity<Journal>()
            .ToTable(nameof(Journal))
            .HasIndex(x => x.CreatedAt);

        modelBuilder.Entity<Journal>()
            .HasIndex(x => x.EventId)
            .HasMethod("hash");
    }
}
