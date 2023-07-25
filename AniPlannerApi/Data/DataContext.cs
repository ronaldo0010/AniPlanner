using AniPlannerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AniPlannerApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Media> Media { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}