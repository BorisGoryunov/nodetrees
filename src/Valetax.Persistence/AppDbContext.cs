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
        modelBuilder.Entity<Node>()
            .HasIndex(x => new { x.TreeId, x.ParentId })
            .IsUnique();
    }
}
