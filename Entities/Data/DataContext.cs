using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;
    public virtual DbSet<Media> Media { get; set; } = null!;
    public DbSet<MediaTag> MediaTags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MediaTag>()
            .HasKey(mt => new { mt.MediaId, mt.TagId });

        modelBuilder.Entity<MediaTag>()
            .HasOne(mt => mt.Media)
            .WithMany(m => m.MediaTags)
            .HasForeignKey(mt => mt.MediaId);

        modelBuilder.Entity<MediaTag>()
            .HasOne(mt => mt.Tag)
            .WithMany(t => t.MediaTags)
            .HasForeignKey(mt => mt.TagId);
    }
}